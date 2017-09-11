using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ShoppingCartSPA1.Controllers
{
    public class Item
    {
        public int Count { get; set; }

        public Product Product { get; set; }
    }
    public class ShoppingCart
    {
        public List<Item> Items { get; set; }

    }
    public class ShoppingCartController : ApiController
    {

        [Route("api/GetProducts")]
        [HttpGet]
        public List<Product> GetProducts()
        {
            var db = new ShoppingCartEntities();
            db.Configuration.ProxyCreationEnabled = false;

            return db.Products.ToList();
        }

        [HttpGet]
        [Route("api/AddToCart/{Id}")]
        public void AddToCart(int Id)
        {
            ShoppingCart sc = null;

            if (HttpContext.Current.Session["sc"] == null)
                sc = new ShoppingCart();
            else
                sc = HttpContext.Current.Session["sc"] as ShoppingCart;

            if (sc.Items == null)
                sc.Items = new List<Item>();

            var db = new ShoppingCartEntities();
            db.Configuration.ProxyCreationEnabled = false;

            var prod = db.Products.Find(Id);

            Item item = new Item();
            item.Count = 1;
            item.Product = prod;

            sc.Items.Add(item);

            HttpContext.Current.Session["sc"] = sc;
        }

        [HttpGet]
        [Route("api/GetCart")]
        public ShoppingCart GetCart()
        {
            return null;//TODO HERE!
        }
    }
}
