﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generation date: 4/26/2020 4:42:28 AM
namespace UdemyAPIOData.API.Models
{
    /// <summary>
    /// There are no comments for CategorySingle in the schema.
    /// </summary>
    public partial class CategorySingle : global::Microsoft.OData.Client.DataServiceQuerySingle<Category>
    {
        /// <summary>
        /// Initialize a new CategorySingle object.
        /// </summary>
        public CategorySingle(global::Microsoft.OData.Client.DataServiceContext context, string path)
            : base(context, path) { }

        /// <summary>
        /// Initialize a new CategorySingle object.
        /// </summary>
        public CategorySingle(global::Microsoft.OData.Client.DataServiceContext context, string path, bool isComposable)
            : base(context, path, isComposable) { }

        /// <summary>
        /// Initialize a new CategorySingle object.
        /// </summary>
        public CategorySingle(global::Microsoft.OData.Client.DataServiceQuerySingle<Category> query)
            : base(query) { }

        /// <summary>
        /// There are no comments for Products in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::Microsoft.OData.Client.DataServiceQuery<global::UdemyAPIOData.API.Models.Product> Products
        {
            get
            {
                if (!this.IsComposable)
                {
                    throw new global::System.NotSupportedException("The previous function is not composable.");
                }
                if ((this._Products == null))
                {
                    this._Products = Context.CreateQuery<global::UdemyAPIOData.API.Models.Product>(GetPath("Products"));
                }
                return this._Products;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::Microsoft.OData.Client.DataServiceQuery<global::UdemyAPIOData.API.Models.Product> _Products;
    }
    /// <summary>
    /// There are no comments for Category in the schema.
    /// </summary>
    /// <KeyProperties>
    /// Id
    /// </KeyProperties>
    [global::Microsoft.OData.Client.Key("Id")]
    public partial class Category : global::Microsoft.OData.Client.BaseEntityType
    {
        /// <summary>
        /// Create a new Category object.
        /// </summary>
        /// <param name="ID">Initial value of Id.</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public static Category CreateCategory(int ID)
        {
            Category category = new Category();
            category.Id = ID;
            return category;
        }
        /// <summary>
        /// There are no comments for Property Id in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this.OnIdChanging(value);
                this._Id = value;
                this.OnIdChanged();
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private int _Id;
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        /// <summary>
        /// There are no comments for Property Name in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this.OnNameChanging(value);
                this._Name = value;
                this.OnNameChanged();
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _Name;
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        /// <summary>
        /// There are no comments for Property Products in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Collections.ObjectModel.Collection<global::UdemyAPIOData.API.Models.Product> Products
        {
            get
            {
                return this._Products;
            }
            set
            {
                this.OnProductsChanging(value);
                this._Products = value;
                this.OnProductsChanged();
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Collections.ObjectModel.Collection<global::UdemyAPIOData.API.Models.Product> _Products = new global::System.Collections.ObjectModel.Collection<global::UdemyAPIOData.API.Models.Product>();
        partial void OnProductsChanging(global::System.Collections.ObjectModel.Collection<global::UdemyAPIOData.API.Models.Product> value);
        partial void OnProductsChanged();
    }
    /// <summary>
    /// There are no comments for ProductSingle in the schema.
    /// </summary>
    public partial class ProductSingle : global::Microsoft.OData.Client.DataServiceQuerySingle<Product>
    {
        /// <summary>
        /// Initialize a new ProductSingle object.
        /// </summary>
        public ProductSingle(global::Microsoft.OData.Client.DataServiceContext context, string path)
            : base(context, path) { }

        /// <summary>
        /// Initialize a new ProductSingle object.
        /// </summary>
        public ProductSingle(global::Microsoft.OData.Client.DataServiceContext context, string path, bool isComposable)
            : base(context, path, isComposable) { }

        /// <summary>
        /// Initialize a new ProductSingle object.
        /// </summary>
        public ProductSingle(global::Microsoft.OData.Client.DataServiceQuerySingle<Product> query)
            : base(query) { }

        /// <summary>
        /// There are no comments for Category in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::UdemyAPIOData.API.Models.CategorySingle Category
        {
            get
            {
                if (!this.IsComposable)
                {
                    throw new global::System.NotSupportedException("The previous function is not composable.");
                }
                if ((this._Category == null))
                {
                    this._Category = new global::UdemyAPIOData.API.Models.CategorySingle(this.Context, GetPath("Category"));
                }
                return this._Category;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::UdemyAPIOData.API.Models.CategorySingle _Category;
    }
    /// <summary>
    /// There are no comments for Product in the schema.
    /// </summary>
    /// <KeyProperties>
    /// Id
    /// </KeyProperties>
    [global::Microsoft.OData.Client.Key("Id")]
    public partial class Product : global::Microsoft.OData.Client.BaseEntityType
    {
        /// <summary>
        /// Create a new Product object.
        /// </summary>
        /// <param name="ID">Initial value of Id.</param>
        /// <param name="stock">Initial value of Stock.</param>
        /// <param name="price">Initial value of Price.</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public static Product CreateProduct(int ID, int stock, int price)
        {
            Product product = new Product();
            product.Id = ID;
            product.Stock = stock;
            product.Price = price;
            return product;
        }
        /// <summary>
        /// There are no comments for Property Id in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this.OnIdChanging(value);
                this._Id = value;
                this.OnIdChanged();
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private int _Id;
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        /// <summary>
        /// There are no comments for Property Name in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this.OnNameChanging(value);
                this._Name = value;
                this.OnNameChanged();
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _Name;
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        /// <summary>
        /// There are no comments for Property Stock in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual int Stock
        {
            get
            {
                return this._Stock;
            }
            set
            {
                this.OnStockChanging(value);
                this._Stock = value;
                this.OnStockChanged();
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private int _Stock;
        partial void OnStockChanging(int value);
        partial void OnStockChanged();
        /// <summary>
        /// There are no comments for Property Price in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual int Price
        {
            get
            {
                return this._Price;
            }
            set
            {
                this.OnPriceChanging(value);
                this._Price = value;
                this.OnPriceChanged();
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private int _Price;
        partial void OnPriceChanging(int value);
        partial void OnPriceChanged();
        /// <summary>
        /// There are no comments for Property CategoryId in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<int> CategoryId
        {
            get
            {
                return this._CategoryId;
            }
            set
            {
                this.OnCategoryIdChanging(value);
                this._CategoryId = value;
                this.OnCategoryIdChanged();
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<int> _CategoryId;
        partial void OnCategoryIdChanging(global::System.Nullable<int> value);
        partial void OnCategoryIdChanged();
        /// <summary>
        /// There are no comments for Property Created in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.DateTimeOffset> Created
        {
            get
            {
                return this._Created;
            }
            set
            {
                this.OnCreatedChanging(value);
                this._Created = value;
                this.OnCreatedChanged();
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.DateTimeOffset> _Created;
        partial void OnCreatedChanging(global::System.Nullable<global::System.DateTimeOffset> value);
        partial void OnCreatedChanged();
        /// <summary>
        /// There are no comments for Property Category in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::UdemyAPIOData.API.Models.Category Category
        {
            get
            {
                return this._Category;
            }
            set
            {
                this.OnCategoryChanging(value);
                this._Category = value;
                this.OnCategoryChanged();
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::UdemyAPIOData.API.Models.Category _Category;
        partial void OnCategoryChanging(global::UdemyAPIOData.API.Models.Category value);
        partial void OnCategoryChanged();
    }
    /// <summary>
    /// There are no comments for Login in the schema.
    /// </summary>
    public partial class Login
    {
        /// <summary>
        /// There are no comments for Property Email in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual string Email
        {
            get
            {
                return this._Email;
            }
            set
            {
                this.OnEmailChanging(value);
                this._Email = value;
                this.OnEmailChanged();
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _Email;
        partial void OnEmailChanging(string value);
        partial void OnEmailChanged();
        /// <summary>
        /// There are no comments for Property Password in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual string Password
        {
            get
            {
                return this._Password;
            }
            set
            {
                this.OnPasswordChanging(value);
                this._Password = value;
                this.OnPasswordChanged();
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _Password;
        partial void OnPasswordChanging(string value);
        partial void OnPasswordChanged();
    }
    /// <summary>
    /// Class containing all extension methods
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Get an entity of type global::UdemyAPIOData.API.Models.Category as global::UdemyAPIOData.API.Models.CategorySingle specified by key from an entity set
        /// </summary>
        /// <param name="source">source entity set</param>
        /// <param name="keys">dictionary with the names and values of keys</param>
        public static global::UdemyAPIOData.API.Models.CategorySingle ByKey(this global::Microsoft.OData.Client.DataServiceQuery<global::UdemyAPIOData.API.Models.Category> source, global::System.Collections.Generic.IDictionary<string, object> keys)
        {
            return new global::UdemyAPIOData.API.Models.CategorySingle(source.Context, source.GetKeyPath(global::Microsoft.OData.Client.Serializer.GetKeyString(source.Context, keys)));
        }
        /// <summary>
        /// Get an entity of type global::UdemyAPIOData.API.Models.Category as global::UdemyAPIOData.API.Models.CategorySingle specified by key from an entity set
        /// </summary>
        /// <param name="source">source entity set</param>
        /// <param name="id">The value of id</param>
        public static global::UdemyAPIOData.API.Models.CategorySingle ByKey(this global::Microsoft.OData.Client.DataServiceQuery<global::UdemyAPIOData.API.Models.Category> source,
            int id)
        {
            global::System.Collections.Generic.IDictionary<string, object> keys = new global::System.Collections.Generic.Dictionary<string, object>
            {
                { "Id", id }
            };
            return new global::UdemyAPIOData.API.Models.CategorySingle(source.Context, source.GetKeyPath(global::Microsoft.OData.Client.Serializer.GetKeyString(source.Context, keys)));
        }
        /// <summary>
        /// Get an entity of type global::UdemyAPIOData.API.Models.Product as global::UdemyAPIOData.API.Models.ProductSingle specified by key from an entity set
        /// </summary>
        /// <param name="source">source entity set</param>
        /// <param name="keys">dictionary with the names and values of keys</param>
        public static global::UdemyAPIOData.API.Models.ProductSingle ByKey(this global::Microsoft.OData.Client.DataServiceQuery<global::UdemyAPIOData.API.Models.Product> source, global::System.Collections.Generic.IDictionary<string, object> keys)
        {
            return new global::UdemyAPIOData.API.Models.ProductSingle(source.Context, source.GetKeyPath(global::Microsoft.OData.Client.Serializer.GetKeyString(source.Context, keys)));
        }
        /// <summary>
        /// Get an entity of type global::UdemyAPIOData.API.Models.Product as global::UdemyAPIOData.API.Models.ProductSingle specified by key from an entity set
        /// </summary>
        /// <param name="source">source entity set</param>
        /// <param name="id">The value of id</param>
        public static global::UdemyAPIOData.API.Models.ProductSingle ByKey(this global::Microsoft.OData.Client.DataServiceQuery<global::UdemyAPIOData.API.Models.Product> source,
            int id)
        {
            global::System.Collections.Generic.IDictionary<string, object> keys = new global::System.Collections.Generic.Dictionary<string, object>
            {
                { "Id", id }
            };
            return new global::UdemyAPIOData.API.Models.ProductSingle(source.Context, source.GetKeyPath(global::Microsoft.OData.Client.Serializer.GetKeyString(source.Context, keys)));
        }
    }
}
namespace Default
{
    /// <summary>
    /// There are no comments for Container in the schema.
    /// </summary>
    public partial class Container : global::Microsoft.OData.Client.DataServiceContext
    {
        /// <summary>
        /// Initialize a new Container object.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public Container(global::System.Uri serviceRoot) :
                base(serviceRoot, global::Microsoft.OData.Client.ODataProtocolVersion.V4)
        {
            this.OnContextCreated();
            this.Format.LoadServiceModel = GeneratedEdmModel.GetInstance;
            this.Format.UseJson();
        }
        partial void OnContextCreated();
        /// <summary>
        /// There are no comments for Categories in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::Microsoft.OData.Client.DataServiceQuery<global::UdemyAPIOData.API.Models.Category> Categories
        {
            get
            {
                if ((this._Categories == null))
                {
                    this._Categories = base.CreateQuery<global::UdemyAPIOData.API.Models.Category>("Categories");
                }
                return this._Categories;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::Microsoft.OData.Client.DataServiceQuery<global::UdemyAPIOData.API.Models.Category> _Categories;
        /// <summary>
        /// There are no comments for Products in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::Microsoft.OData.Client.DataServiceQuery<global::UdemyAPIOData.API.Models.Product> Products
        {
            get
            {
                if ((this._Products == null))
                {
                    this._Products = base.CreateQuery<global::UdemyAPIOData.API.Models.Product>("Products");
                }
                return this._Products;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::Microsoft.OData.Client.DataServiceQuery<global::UdemyAPIOData.API.Models.Product> _Products;
        /// <summary>
        /// There are no comments for Categories in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual void AddToCategories(global::UdemyAPIOData.API.Models.Category category)
        {
            base.AddObject("Categories", category);
        }
        /// <summary>
        /// There are no comments for Products in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual void AddToProducts(global::UdemyAPIOData.API.Models.Product product)
        {
            base.AddObject("Products", product);
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private abstract class GeneratedEdmModel
        {
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
            private static global::Microsoft.OData.Edm.IEdmModel ParsedModel = LoadModelFromString();

            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
            private const string filePath = @"Csdl.xml";

            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
            public static global::Microsoft.OData.Edm.IEdmModel GetInstance()
            {
                return ParsedModel;
            }
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
            private static global::Microsoft.OData.Edm.IEdmModel LoadModelFromString()
            {
                global::System.Xml.XmlReader reader = CreateXmlReader();
                try
                {
                    global::System.Collections.Generic.IEnumerable<global::Microsoft.OData.Edm.Validation.EdmError> errors;
                    global::Microsoft.OData.Edm.IEdmModel edmModel;

                    if (!global::Microsoft.OData.Edm.Csdl.CsdlReader.TryParse(reader, false, out edmModel, out errors))
                    {
                        global::System.Text.StringBuilder errorMessages = new System.Text.StringBuilder();
                        foreach (var error in errors)
                        {
                            errorMessages.Append(error.ErrorMessage);
                            errorMessages.Append("; ");
                        }
                        throw new global::System.InvalidOperationException(errorMessages.ToString());
                    }

                    return edmModel;
                }
                finally
                {
                    ((global::System.IDisposable)(reader)).Dispose();
                }
            }
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
            private static global::System.Xml.XmlReader CreateXmlReader(string edmxToParse)
            {
                return global::System.Xml.XmlReader.Create(new global::System.IO.StringReader(edmxToParse));
            }

            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
            private static global::System.Xml.XmlReader CreateXmlReader()
            {
                try
                {
                    var assembly = global::System.Reflection.Assembly.GetExecutingAssembly();
                    var resourcePath = global::System.Linq.Enumerable.Single(assembly.GetManifestResourceNames(), str => str.EndsWith(filePath));
                    global::System.IO.Stream stream = assembly.GetManifestResourceStream(resourcePath);
                    return global::System.Xml.XmlReader.Create(new global::System.IO.StreamReader(stream));
                }
                catch (global::System.Xml.XmlException e)
                {
                    throw new global::System.Xml.XmlException("Failed to create an XmlReader from the stream. Check if the resource exists.", e);
                }
            }
        }
        /// <summary>
        /// There are no comments for GetKdv in the schema.
        /// </summary>
        public virtual global::Microsoft.OData.Client.DataServiceQuerySingle<int> GetKdv()
        {
            return this.CreateFunctionQuerySingle<int>("", "GetKdv", false);
        }
    }
    /// <summary>
    /// Class containing all extension methods
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// There are no comments for CategoryCount in the schema.
        /// </summary>
        public static global::Microsoft.OData.Client.DataServiceQuerySingle<int> CategoryCount(this global::Microsoft.OData.Client.DataServiceQuery<global::UdemyAPIOData.API.Models.Category> source)
        {
            if (!source.IsComposable)
            {
                throw new global::System.NotSupportedException("The previous function is not composable.");
            }

            return source.CreateFunctionQuerySingle<int>("Default.CategoryCount", false);
        }
        /// <summary>
        /// There are no comments for MultiplyFunction in the schema.
        /// </summary>
        public static global::Microsoft.OData.Client.DataServiceQuerySingle<int> MultiplyFunction(this global::Microsoft.OData.Client.DataServiceQuery<global::UdemyAPIOData.API.Models.Product> source, int a1, int a2, int a3)
        {
            if (!source.IsComposable)
            {
                throw new global::System.NotSupportedException("The previous function is not composable.");
            }

            return source.CreateFunctionQuerySingle<int>("Default.MultiplyFunction", false, new global::Microsoft.OData.Client.UriOperationParameter("a1", a1),
                    new global::Microsoft.OData.Client.UriOperationParameter("a2", a2),
                    new global::Microsoft.OData.Client.UriOperationParameter("a3", a3));
        }
        /// <summary>
        /// There are no comments for KdvHesapla in the schema.
        /// </summary>
        public static global::Microsoft.OData.Client.DataServiceQuerySingle<double> KdvHesapla(this global::Microsoft.OData.Client.DataServiceQuerySingle<global::UdemyAPIOData.API.Models.Product> source, double kdv)
        {
            if (!source.IsComposable)
            {
                throw new global::System.NotSupportedException("The previous function is not composable.");
            }

            return source.CreateFunctionQuerySingle<double>("Default.KdvHesapla", false, new global::Microsoft.OData.Client.UriOperationParameter("kdv", kdv));
        }
        /// <summary>
        /// There are no comments for TotalProductPrice in the schema.
        /// </summary>
        public static global::Microsoft.OData.Client.DataServiceActionQuerySingle<int> TotalProductPrice(this global::Microsoft.OData.Client.DataServiceQuerySingle<global::UdemyAPIOData.API.Models.Category> source)
        {
            if (!source.IsComposable)
            {
                throw new global::System.NotSupportedException("The previous function is not composable.");
            }

            return new global::Microsoft.OData.Client.DataServiceActionQuerySingle<int>(source.Context, source.AppendRequestUri("Default.TotalProductPrice"));
        }
        /// <summary>
        /// There are no comments for TotalProductPrice2 in the schema.
        /// </summary>
        public static global::Microsoft.OData.Client.DataServiceActionQuerySingle<int> TotalProductPrice2(this global::Microsoft.OData.Client.DataServiceQuery<global::UdemyAPIOData.API.Models.Category> source)
        {
            if (!source.IsComposable)
            {
                throw new global::System.NotSupportedException("The previous function is not composable.");
            }

            return new global::Microsoft.OData.Client.DataServiceActionQuerySingle<int>(source.Context, source.AppendRequestUri("Default.TotalProductPrice2"));
        }
        /// <summary>
        /// There are no comments for TotalProductPriceWithParametre in the schema.
        /// </summary>
        public static global::Microsoft.OData.Client.DataServiceActionQuerySingle<int> TotalProductPriceWithParametre(this global::Microsoft.OData.Client.DataServiceQuery<global::UdemyAPIOData.API.Models.Category> source, int categoryId)
        {
            if (!source.IsComposable)
            {
                throw new global::System.NotSupportedException("The previous function is not composable.");
            }

            return new global::Microsoft.OData.Client.DataServiceActionQuerySingle<int>(source.Context, source.AppendRequestUri("Default.TotalProductPriceWithParametre"), new global::Microsoft.OData.Client.BodyOperationParameter("categoryId", categoryId));
        }
        /// <summary>
        /// There are no comments for Total in the schema.
        /// </summary>
        public static global::Microsoft.OData.Client.DataServiceActionQuerySingle<int> Total(this global::Microsoft.OData.Client.DataServiceQuery<global::UdemyAPIOData.API.Models.Category> source, int a, int b, int c)
        {
            if (!source.IsComposable)
            {
                throw new global::System.NotSupportedException("The previous function is not composable.");
            }

            return new global::Microsoft.OData.Client.DataServiceActionQuerySingle<int>(source.Context, source.AppendRequestUri("Default.Total"), new global::Microsoft.OData.Client.BodyOperationParameter("a", a),
                    new global::Microsoft.OData.Client.BodyOperationParameter("b", b),
                    new global::Microsoft.OData.Client.BodyOperationParameter("c", c));
        }
        /// <summary>
        /// There are no comments for LoginUser in the schema.
        /// </summary>
        public static global::Microsoft.OData.Client.DataServiceActionQuerySingle<string> LoginUser(this global::Microsoft.OData.Client.DataServiceQuery<global::UdemyAPIOData.API.Models.Product> source, global::UdemyAPIOData.API.Models.Login UserLogin)
        {
            if (!source.IsComposable)
            {
                throw new global::System.NotSupportedException("The previous function is not composable.");
            }

            return new global::Microsoft.OData.Client.DataServiceActionQuerySingle<string>(source.Context, source.AppendRequestUri("Default.LoginUser"), new global::Microsoft.OData.Client.BodyOperationParameter("UserLogin", UserLogin));
        }
    }
}