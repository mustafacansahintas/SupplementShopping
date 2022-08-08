using BLL;
using BLL.Result;
using Common.Helper;
using Entities.Concrete;
using Entities.Message;
using Project.Models;
using Project.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class HomeController : Controller
    {

       private UserManager um=new UserManager();
       private ProductManager pm =new ProductManager();
       private OrderManager om=new OrderManager();
       private OrderLineManager Orderline=new OrderLineManager();

        // GET: Home
        public ActionResult Index(int? id)
        {
            if (id==null)
            {
               return View(pm.List());
            }
            else
            {
                var liste=pm.List().Where(x=>x.category.Id==id).ToList();

                if (liste.Count==0)
                {
                    return RedirectToAction("SearchResult");
                }


                return View(liste);
            }
            
            
            
        }

        public ActionResult GetBrands(int? id)
        {
            if (id == null)
            {
                return View();
            }
            else
            {
                var liste = pm.List().Where(x => x.brand.Id == id).ToList();

                if (liste.Count == 0)
                {
                    return RedirectToAction("SearchResult");
                }


                return View("Index",liste);
            }



        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<_User> bll=um.LoginUser(model);

                if (bll.Errors.Count>0)
                {
                    if (bll.Errors.Find(x => x.Code == ErrorMessageCode.UserIsNotActive) != null)
                    {
                        ViewBag.SetLink = "http://Home/Activate/1234-567-78890";
                    }
                    bll.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }
                CurrentSession.Set<_User>("login", bll.Result);
                if (CurrentSession.user.isAdmin)
                {
                    return RedirectToAction("Index","Admin");
                }
                return RedirectToAction("Index"); 
                
            }

            return View(model);
        }

        public ActionResult Register()
        {

            return View();
        }
        
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            // Kullanıcı username kontrolü
            // Kullanıcı email kontrolü
            // Kayıt İşlemi..
            // Aktivasyon e-postası gönderildi

            if (ModelState.IsValid)
            {
                BusinessLayerResult<_User> res = um.RegisterUser(model);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }

                OkViewModel notifyObj = new OkViewModel()
                {
                    Title = "Kayıt Başarılı",
                    RedirectingUrl = "/Home/Login"
                };
                notifyObj.Items.Add("Lütfen email adresinize gönderilen aktivasyon link'ine tıklayrak hesabınızı aktifleştiriniz. Hesabınızı aktive etmeden alışveriş ve beğeni yapamazsınız.");

                

                return View("Ok", notifyObj);
            }

            return View(model);
        }

        public ActionResult UserActivate(Guid id)
        {
            // Kullanıcı aktivasyonu sağlanacak
            BusinessLayerResult<_User> res = um.ActivateUser(id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Title = "Geçersiz işlem",
                    Items = res.Errors
                };
                return View("Error", errorNotifyObj);
            }

            OkViewModel okNotifyObj = new OkViewModel()
            {
                Title = "Hesap Aktifleştirildi",
                RedirectingUrl = "/Home/Login"
            };

            okNotifyObj.Items.Add("Hesabınız aktifleştirildi");

            return View("Ok", okNotifyObj);
        }

        public ActionResult UserUpdate(int id)
        {
            var user=um.Find(x=> x.Id == id);

            return View(user);
        }

        [HttpPost]
        public ActionResult UserUpdate(_User user)
        {
            if (ModelState.IsValid)
            {

                _User User=um.Find(x=> x.Id==user.Id);
                User.Name=user.Name;
                User.Surname=user.Surname;
                User.UserName=user.UserName;
                User.Password=user.Password;
                User.Email=user.Email;
                user.Gender = user.Gender;

                um.Update(User);

                return View(User);

            }
                return View(user);
        }

        public ActionResult ProductDetail(int? id)
        {

            Product product=pm.Find(x=> x.Id == id);

            return View(product);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
        public ActionResult Account(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            _User user=um.Find(x=>x.Id == id);

            if (user==null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        public ActionResult AccountDelete(int? id)
        {
         
            _User user = um.Find(u => u.Id == id);
         
             um.Delete(user);
            
            return RedirectToAction("AccountDeleteInfo");
        }

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(FormCollection formCollection)
        {  
            string SearchText=formCollection["SearchText"];

            List<Product> list = new List<Product>();
            var liste = pm.List();

            if (SearchText=="")
            {
                return View(liste);
            }

           

            foreach (var item in liste)
            {
                if (item.ProductName.ToLower().Contains(SearchText.ToLower()))
                {
                    list.Add(item);
                }
            }

           

            if (list.Count==0)
            {
                return RedirectToAction("SearchResult");
            }

            return View(list);
        }

        public ActionResult SearchResult()
        {
            return View();
        }

        public ActionResult AccountDeleteInfo()
        {
            Session.Clear();

            return View();
        }

      
        public ActionResult OrderView(int id)
        {

            var liste=Orderline.List().Where(x=> x._UserId == id).ToList();

            return View(liste);
        }

        public ActionResult OrderDelete(int id)
        {
            foreach (var item in Orderline.List().Where(x=> x.OrderId==id))
            {
                Orderline.Delete(item);
            }

            var order=om.Find(x=>x.Id == id);

            om.Delete(order);

            var liste = Orderline.List().Where(x => x._UserId == CurrentSession.user.Id).ToList();

            return RedirectToAction("OrderView","Home",new { @id = CurrentSession.user.Id });
        }

        public ActionResult ShoppingCart(int id,int quantity,string ViewId)
        {

            var product=pm.Find(x=> x.Id == id);

            if (product!=null)
            {
                GetCart().AddProduct(product, quantity);
            }
            if (ViewId=="ProductDetail")
            {

                TempData["adet"] = GetCart().CartLines;

                return RedirectToAction("ProductDetail", product);

                
            }

            else if (ViewId=="Cart")
            {
                return RedirectToAction("MyShopCart", product);
            }
               
            return RedirectToAction("Index");
        }

        public Cart GetCart()
        {
            var cart = (Cart)Session["Cart"];

            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public ActionResult DeleteProductCart(int id)
        {
            var product = pm.Find(x=> x.Id==id);

            if (product != null)
            {
                GetCart().DeleteProduct(product);
            }

            return RedirectToAction("MyShopCart");


        }

        public PartialViewResult Summary()
        {
            return PartialView(GetCart());
        }

        public ActionResult MyShopCart()
        {
            return View(GetCart());
        }

        public ActionResult OrderCheckout()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderCheckout(Order order)
        {

            if (ModelState.IsValid)
            { 
                order.Total=GetCart().Total();
                order.OrderCode = new Guid();
                order.OrderDate = new DateTime(2000, 2, 2, 10, 10, 10);
                order.Username = CurrentSession.user.UserName;
        
                om.Insert(order);

                foreach (var item in GetCart().CartLines)
                {
                    OrderLine line = new OrderLine();

                    line.OrderId = order.Id;

                    line.Quantity=item.Quantity;

                    line.UnitPrice = item.product.UnitPrice;

                    line.ProductId = item.product.Id;

                    line._UserId=CurrentSession.user.Id;

                    Orderline.Insert(line);

                  line.Product.Stock -= item.Quantity;

                    pm.Update(line.Product);

                }

                GetCart().Clear();

                return RedirectToAction("OrderComplete");
            }

            return View();
           
        }

        public ActionResult Quantityİncrease(int id)
        {
            var product=GetCart().CartLines.Where(x=> x.product.Id==id).FirstOrDefault();

            if (product.Quantity>1)
            {

            product.Quantity--;

            return RedirectToAction("MyShopCart",product);
            
            }

            return RedirectToAction("MyShopCart");
        }

        public ActionResult OrderComplete()
        {
            return View();
        }

        

      
    }
}