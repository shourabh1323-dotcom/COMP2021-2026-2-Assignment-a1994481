using System.Collections.Generic;
using RecipeManagement.Core;

namespace RecipeManagement.Tests;

/// <summary>
/// Example tests from the assignment specification. Add your own tests as you work.
/// </summary>
public sealed class RecipeManagerTests
{
    [Fact]
    public void Constructor_BuildsRecipeDictionary()
    {
        var manager = CreateManager();
        Assert.Equal(2, manager.RecipeCount);
        Assert.Equal("Recipe A", manager.FindRecipe(10)?.Title);
    }

    [Fact]
    public void InstructionsAreCompletedInFileOrder()
    {
        var manager = CreateManager();
        Assert.True(manager.StartCooking(10));
        Assert.Equal("First step", manager.PeekNextInstruction());
        Assert.Equal("First step", manager.CompleteNextInstruction());
        Assert.Equal("Second step", manager.PeekNextInstruction());
    }

    [Fact]
    public void RemovedRecipesAreRestoredLastInFirstOut()
    {
        var manager = CreateManager();
        manager.AddRecipeToCookingPlan(10);
        manager.AddRecipeToCookingPlan(20);
        manager.RemoveRecipeFromCookingPlan(10);
        manager.RemoveRecipeFromCookingPlan(20);
        Assert.Equal(20, manager.PeekLastRemovedRecipe());
        Assert.True(manager.RestoreLastRemovedRecipe());
        Assert.Equal(new[] { 20 }, manager.GetCookingPlan());
    }

    [Fact]
    public void AddRecipe_NewId_ReturnsTrueAndCanBeFound() {
        var manager = CreateManager();
        var recipe = new Recipe{Id = 2, Title = "Khichuri"};

        bool result = manager.AddRecipe(recipe);

        Assert.True(result);
        Assert.Equal("Khichuri", manager.FindRecipe(2)?.Title);
    }

    [Fact]
    public void AddRecipe_DuplicateId_ReturnsFalse() {
        var manager = CreateManager();
        var recipe = new Recipe{Id = 10, Title = "Khichuri"};

        bool result = manager.AddRecipe(recipe);

        Assert.False(result);
    }

    [Fact]
    public void FindRecipe_MissingId_ReturnsNull() {
        var manager = CreateManager();
        Assert.Null(manager.FindRecipe(999));
    }

    [Fact]
    public void RemoveRecipe_MissingId_ReturnsFalse(){
        var manager = CreateManager();
        Assert.False(manager.RemoveRecipe(999));
        }

        [Fact]
        public void Constructor_DuplicateId_ThrowsArgumentException(){
            var recipes = new[]{
                new Recipe{Id = 233, Title = "Fried Rice"},
                new Recipe{Id = 233, Title = "Fish Fry"}
            };
            Assert.Throws<ArgumentException>(() => new RecipeManager(recipes));
        }
        [Fact]
        public void AddIngredientsToShoppingList_MissingId_ReturnsZero(){
            var manager = CreateManager();
            Assert.Equal(0, manager.AddIngredientsToShoppingList(999));
        }
        [Fact]
        public void AddIngredientsToShoppingList_ExistingRecipe_ReturnsIngredientCount(){
            var manager = CreateManager();
            Assert.Equal(1, manager.AddIngredientsToShoppingList(10));
        }

        [Fact]
        public void GetShoppingList_AfterAddingRecipe_ContainsIngredients(){
            var manager = CreateManager();
            manager.AddIngredientsToShoppingList(10);
            Assert.Equal(new[]{"1 apple"}, manager.GetShoppingList());
        }

        [Fact]
        public void ClearShoppingList_AfterAdding_ListIsEmpty(){
            var manager = CreateManager();
            manager.AddIngredientsToShoppingList(10);
            manager.ClearShoppingList();
            Assert.Empty(manager.GetShoppingList());
        }

        [Fact]
        public void AddRecipeToCookingPlan_ExistingRecipe_AddsToPlan(){
            var manager = CreateManager();
            Assert.True(manager.AddRecipeToCookingPlan(10));
            Assert.Equal(new[]{10}, manager.GetCookingPlan());
        }

        [Fact]
        public void AddRecipeToCookingPlan_AlreadyPlanned_ReturnsFalse(){
            var manager = CreateManager();
            Assert.True(manager.AddRecipeToCookingPlan(10));
            Assert.False(manager.AddRecipeToCookingPlan(10));
        }

        [Fact]
        public void AddRecipeToCookingPlan_MissingRecipe_ReturnsFalse(){
            var manager = CreateManager();
            Assert.False(manager.AddRecipeToCookingPlan(999));
        }

        [Fact]
        public void RemoveRecipeFromCookingPlan_NotPlanned_ReturnsFalse(){
            var manager = CreateManager();
            Assert.False(manager.RemoveRecipeFromCookingPlan(10));
        }

        [Fact]
        public void PeekLastRemovedRecipe_EmptyStack_ReturnsNull(){
            var manager = CreateManager();
            Assert.Null(manager.PeekLastRemovedRecipe());
        }

        [Fact]
        public void RestoreLastRemovedRecipe_EmptyStack_ReturnsFalse(){
            var manager = CreateManager();
            Assert.False(manager.RestoreLastRemovedRecipe());
        }

        [Fact]
        public void RemoveRecipe_WhenInCookingPlan_ReturnsFalse(){
            var manager = CreateManager();
            manager.AddRecipeToCookingPlan(10);
            Assert.False(manager.RemoveRecipe(10));
            Assert.NotNull(manager.FindRecipe(10));
        }

    private static RecipeManager CreateManager()
    {
        return new RecipeManager(new[]
        {
            new Recipe
            {
                Id = 10,
                Title = "Recipe A",
                Ingredients = new() { "1 apple" },
                Instructions = new() { "First step", "Second step" }
            },
            new Recipe
            {
                Id = 20,
                Title = "Recipe B"
            }
        });
    }
}
