using OnlineShop_AspNetCore_ReactJS.Data.Entities;
using System;
using System.Linq;

namespace OnlineShop_AspNetCore_ReactJS.Data
{
    public class SeedDatabase
    {
        public static Tuple<Banner[], Category[], Pie[], Iteration[], WorkItem[]> Initialize(OnlineShopContext context)
        {
            context.Database.EnsureCreated();

            Banner[] banners = null;
            if (!context.Banner.Any())
            {
                banners = new Banner[] {
                    new Banner { Id = 1, Name = "Carousel1", Description = "We sell the best pies in the town!", ImageUrl = "/images/carousel1.jpg" },
                    new Banner { Id = 2, Name = "Carousel2", Description = "We sell the best pies in the town!", ImageUrl = "/images/carousel2.jpg" },
                    new Banner { Id = 3, Name = "Carousel3", Description = "We sell the best pies in the town!", ImageUrl = "/images/carousel3.jpg" }
                };
                context.Banner.AddRange(banners);
            }

            Category[] categories = null;
            Category categoryFruitPies = new Category { Id = 1, Name = "Fruit pies", Description = "Fruit pies" };
            Category categoryCheeseCakes = new Category { Id = 2, Name = "Cheese cakes", Description = "Cheese cakes" };
            Category categorySeasonalPies = new Category { Id = 3, Name = "Seasonal pies", Description = "Seasonal pies" };
            if (!context.Category.Any())
            {
                categories = new Category[] { categoryFruitPies, categoryCheeseCakes, categorySeasonalPies };
                context.Category.AddRange(categories);
            }

            Pie[] pies = null;
            if (!context.Pie.Any())
            {
                string longDescription = "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.";

                pies = new Pie[] {
                    new Pie
                    {
                        Id = 1,
                        Name = "Apple Pie",
                        Price = 12.95M,
                        ShortDescription = "Our famous apple pies!",
                        LongDescription = longDescription,
                        ImageUrl = "/images/pies/applepie.jpg",
                        ThumbnailImageUrl = "/images/pies/thumbnails/applepiesmall.jpg",
                        InStock = true,
                        IsPieOfTheWeek = true,
                        Category = categoryFruitPies
                    },
                    new Pie
                    {
                        Id = 2,
                        Name = "Blueberry Cheese Cake",
                        Price = 18.95M,
                        ShortDescription = "You'll love it!",
                        LongDescription = longDescription,
                        ImageUrl = "/images/pies/blueberrycheesecake.jpg",
                        ThumbnailImageUrl = "/images/pies/thumbnails/blueberrycheesecakesmall.jpg",
                        InStock = true,
                        IsPieOfTheWeek = false,
                        Category = categoryCheeseCakes
                    },
                    new Pie
                    {
                        Id = 3,
                        Name = "Cheese Cake",
                        Price = 18.95M,
                        ShortDescription = "Plain cheese cake. Plain pleasure.",
                        LongDescription = longDescription,
                        ImageUrl = "/images/pies/cheesecake.jpg",
                        ThumbnailImageUrl = "/images/pies/thumbnails/cheesecakesmall.jpg",
                        InStock = true,
                        IsPieOfTheWeek = false,
                        Category = categoryCheeseCakes
                    },
                    new Pie
                    {
                        Id = 4,
                        Name = "Cherry Pie",
                        Price = 15.95M,
                        ShortDescription = "A summer classic!",
                        LongDescription = longDescription,
                        ImageUrl = "/images/pies/cherrypie.jpg",
                        ThumbnailImageUrl = "/images/pies/thumbnails/cherrypiesmall.jpg",
                        InStock = true,
                        IsPieOfTheWeek = false,
                        Category = categoryFruitPies
                    },
                    new Pie
                    {
                        Id = 5,
                        Name = "Christmas Apple Pie",
                        Price = 13.95M,
                        ShortDescription = "Happy holidays with this pie!",
                        LongDescription = longDescription,
                        ImageUrl = "/images/pies/christmasapplepie.jpg",
                        ThumbnailImageUrl = "/images/pies/thumbnails/christmasapplepiesmall.jpg",
                        InStock = true,
                        IsPieOfTheWeek = false,
                        Category = categorySeasonalPies
                    },
                    new Pie
                    {
                        Id = 6,
                        Name = "Cranberry Pie",
                        Price = 17.95M,
                        ShortDescription = "A Christmas favorite",
                        LongDescription = longDescription,
                        ImageUrl = "/images/pies/cranberrypie.jpg",
                        ThumbnailImageUrl = "/images/pies/thumbnails/cranberrypiesmall.jpg",
                        InStock = true,
                        IsPieOfTheWeek = false,
                        Category = categorySeasonalPies
                    },
                    new Pie
                    {
                        Id = 7,
                        Name = "Peach Pie",
                        Price = 15.95M,
                        ShortDescription = "Sweet as peach",
                        LongDescription = longDescription,
                        ImageUrl = "/images/pies/peachpie.jpg",
                        ThumbnailImageUrl = "/images/pies/thumbnails/peachpiesmall.jpg",
                        InStock = false,
                        IsPieOfTheWeek = false,
                        Category = categoryFruitPies
                    },
                    new Pie
                    {
                        Id = 8,
                        Name = "Pumpkin Pie",
                        Price = 12.95M,
                        ShortDescription = "Our Halloween favorite",
                        LongDescription = longDescription,
                        ImageUrl = "/images/pies/pumpkinpie.jpg",
                        ThumbnailImageUrl = "/images/pies/thumbnails/pumpkinpiesmall.jpg",
                        InStock = true,
                        IsPieOfTheWeek = true,
                        Category = categorySeasonalPies
                    },
                    new Pie
                    {
                        Id = 9,
                        Name = "Rhubarb Pie",
                        Price = 15.95M,
                        ShortDescription = "My God, so sweet!",
                        LongDescription = longDescription,
                        ImageUrl = "/images/pies/rhubarbpie.jpg",
                        ThumbnailImageUrl = "/images/pies/thumbnails/rhubarbpiesmall.jpg",
                        InStock = true,
                        IsPieOfTheWeek = true,
                        Category = categoryFruitPies
                    },
                    new Pie
                    {
                        Id = 10,
                        Name = "Strawberry Pie",
                        Price = 15.95M,
                        ShortDescription = "Our delicious strawberry pie!",
                        LongDescription = longDescription,
                        ImageUrl = "/images/pies/strawberrypie.jpg",
                        ThumbnailImageUrl = "/images/pies/thumbnails/strawberrypiesmall.jpg",
                        InStock = true,
                        IsPieOfTheWeek = false,
                        Category = categoryFruitPies
                    },
                    new Pie
                    {
                        Id = 11,
                        Name = "Strawberry Cheese Cake",
                        Price = 18.95M,
                        ShortDescription = "You'll love it!",
                        LongDescription = longDescription,
                        ImageUrl = "/images/pies/strawberrycheesecake.jpg",
                        ThumbnailImageUrl = "/images/pies/thumbnails/strawberrycheesecakesmall.jpg",
                        InStock = false,
                        IsPieOfTheWeek = false,
                        Category = categoryCheeseCakes
                    }
                };
                context.Pie.AddRange(pies);
            }

            Iteration[] iterations = null;
            Iteration iterationApplicationOverview = new Iteration { Id = 1, Name = "Application Overview" };
            Iteration iteration1 = new Iteration { Id = 2, Name = "Iteration 1" };
            Iteration iteration2 = new Iteration { Id = 3, Name = "Iteration 2" };
            Iteration iteration3 = new Iteration { Id = 4, Name = "Iteration 3" };
            if (!context.Iteration.Any())
            {
                iterations = new Iteration[] { iterationApplicationOverview, iteration1, iteration2 };
                context.Iteration.AddRange(iterations);
            }

            WorkItem[] workItems = null;
            if (!context.WorkItem.Any())
            {
                workItems = new WorkItem[] {
                    new WorkItem
                    {
                        Id = 1,
                        Name = "Application Overview",
                        ImageUrl = "",
                        Iteration = iterationApplicationOverview
                    },
                    new WorkItem
                    {
                        Id = 2,
                        Name = "Iteration1 - Azure Board View 1",
                        ImageUrl = "/images/devops/Azure_Board_Iteration1_1.jpg",
                        Iteration = iteration1
                    },
                    new WorkItem
                    {
                        Id = 3,
                        Name = "Iteration1 - Azure Board View 2",
                        ImageUrl = "/images/devops/Azure_Board_Iteration1_2.jpg",
                        Iteration = iteration1
                    },
                    new WorkItem
                    {
                        Id = 4,
                        Name = "Iteration1 - Iteration Board View",
                        ImageUrl = "/images/devops/Azure_Board_Iteration1_3.jpg",
                        Iteration = iteration1
                    },
                    new WorkItem
                    {
                        Id = 5,
                        Name = "Iteration2 - Azure Board View 1",
                        ImageUrl = "/images/devops/Azure_Board_Iteration2_1.jpg",
                        Iteration = iteration2
                    },
                    new WorkItem
                    {
                        Id = 6,
                        Name = "Iteration2 - Azure Board View 2",
                        ImageUrl = "/images/devops/Azure_Board_Iteration2_2.jpg",
                        Iteration = iteration2
                    },
                    new WorkItem
                    {
                        Id = 7,
                        Name = "Iteration2 - Work Item Detail View",
                        ImageUrl = "/images/devops/Azure_Board_Iteration2_3.jpg",
                        Iteration = iteration2
                    },
                    new WorkItem
                    {
                        Id = 8,
                        Name = "Iteration2 - Iteration Board View",
                        ImageUrl = "/images/devops/Azure_Board_Iteration2_4.jpg",
                        Iteration = iteration2
                    },
                    new WorkItem
                    {
                        Id = 9,
                        Name = "Iteration2 - Continuous Integration Build Board View",
                        ImageUrl = "/images/devops/Azure_CI_Build_1.jpg",
                        Iteration = iteration2
                    },
                    new WorkItem
                    {
                        Id = 10,
                        Name = "Iteration2 - Continuous Integration Build Detail View 1",
                        ImageUrl = "/images/devops/Azure_CI_Build_1_Detail_1.jpg",
                        Iteration = iteration2
                    },
                    new WorkItem
                    {
                        Id = 11,
                        Name = "Iteration2 - Continuous Integration Build Detail View 2",
                        ImageUrl = "/images/devops/Azure_CI_Build_1_Detail_2.jpg",
                        Iteration = iteration2
                    },
                    new WorkItem
                    {
                        Id = 12,
                        Name = "Iteration3 - Iteration Board View",
                        ImageUrl = "/images/devops/Azure_Board_Iteration3_1.jpg",
                        Iteration = iteration3
                    },
                    new WorkItem
                    {
                        Id = 13,
                        Name = "Iteration3 - Azure Board View",
                        ImageUrl = "/images/devops/Azure_Board_Iteration3_2.jpg",
                        Iteration = iteration3
                    },
                    new WorkItem
                    {
                        Id = 14,
                        Name = "Iteration3 - Continuous Integration Build Detail View 1",
                        ImageUrl = "/images/devops/Azure_CI_Build_3_Detail_1.jpg",
                        Iteration = iteration3
                    },
                    new WorkItem
                    {
                        Id = 15,
                        Name = "Iteration3 - Continuous Integration Build Detail View 2",
                        ImageUrl = "/images/devops/Azure_CI_Build_3_Detail_2.jpg",
                        Iteration = iteration3
                    },
                    new WorkItem
                    {
                        Id = 16,
                        Name = "Iteration3 - Continuous Integration Test Detail View 1",
                        ImageUrl = "/images/devops/Azure_CI_Test_3_Detail_1.jpg",
                        Iteration = iteration3
                    },
                    new WorkItem
                    {
                        Id = 17,
                        Name = "Iteration3 - Continuous Integration Test Detail View 2",
                        ImageUrl = "/images/devops/Azure_CI_Test_3_Detail_2.jpg",
                        Iteration = iteration3
                    }
                };
                context.WorkItem.AddRange(workItems);
            }

            context.SaveChanges();

            return new Tuple<Banner[], Category[], Pie[], Iteration[], WorkItem[]>(banners, categories, pies, iterations, workItems);
        }
    }
}
