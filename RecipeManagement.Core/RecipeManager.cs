using System;
using System.Collections.Generic;

namespace RecipeManagement.Core;

/// <summary>
/// Implement this class using the five Part A collections as private fields:
/// Dictionary&lt;int, Recipe&gt;, List&lt;string&gt;, LinkedList&lt;int&gt;,
/// Stack&lt;int&gt; and Queue&lt;string&gt;.
/// </summary>
public sealed class RecipeManager : IRecipeManager
{
     private Dictionary<int, Recipe> _recipeById = new();
        private List<string> _shoppingList = new ();
        private LinkedList<int> _cookingPlan = new();
        private Stack<int> _removedRecipeIds = new();
        private Queue<string> _activeCookingInstructions = new();

    public RecipeManager(IEnumerable<Recipe> recipes)
    {
            if (recipes==null){
                throw new ArgumentNullException(nameof(recipes));
            }
            
            foreach (Recipe recipe in recipes)
            {
                if (recipe.Id <= 0){
                    throw new ArgumentException("Recipe ID must be positive.");
                }
                if (string.IsNullOrWhiteSpace(recipe.Title)){
                    throw new ArgumentException("Title must not be blank.");
                }
                if(_recipeById.ContainsKey(recipe.Id)){
                    throw new ArgumentException($"Duplicate Recipe ID: {recipe.Id}");
                }
                _recipeById.Add(recipe.Id, recipe);
            }
        }

    public int RecipeCount => _recipeById.Count;
    public int ShoppingItemCount => _shoppingList.Count;
    public int CookingPlanCount => _cookingPlan.Count;
    public int PendingInstructionCount => _activeCookingInstructions.Count;
    public int RemovedRecipeCount => _removedRecipeIds.Count;

    public bool AddRecipe(Recipe recipe){
        if (recipe is null){
            throw new ArgumentNullException(nameof(recipe));
        }
        if(recipe.Id <= 0){
            return false;
        }
        if(string.IsNullOrWhiteSpace(recipe.Title)){
            return false;
        }
        if(_recipeById.ContainsKey(recipe.Id)){
            return false;
        }
        _recipeById.Add(recipe.Id, recipe);
        return true;
    }

    public Recipe? FindRecipe(int recipeId) {
        if (_recipeById.TryGetValue(recipeId, out Recipe? recipe)){
            return recipe;
        }
        else {
        return null;
        }
    }

    public bool RemoveRecipe(int recipeId){
        if(!_recipeById.ContainsKey(recipeId)){
            return false;
        }
        if(_cookingPlan.Contains(recipeId)){
            return false;
        }
        _recipeById.Remove(recipeId);
        return true;
    }

    public int AddIngredientsToShoppingList(int recipeId) =>
        throw new NotImplementedException("Part A: implement AddIngredientsToShoppingList.");

    public IReadOnlyList<string> GetShoppingList() =>
        throw new NotImplementedException("Part A: implement GetShoppingList.");

    public void ClearShoppingList() =>
        throw new NotImplementedException("Part A: implement ClearShoppingList.");

    public bool AddRecipeToCookingPlan(int recipeId) =>
        throw new NotImplementedException("Part A: implement AddRecipeToCookingPlan.");

    public bool RemoveRecipeFromCookingPlan(int recipeId) =>
        throw new NotImplementedException("Part A: implement RemoveRecipeFromCookingPlan.");

    public bool RestoreLastRemovedRecipe() =>
        throw new NotImplementedException("Part A: implement RestoreLastRemovedRecipe.");

    public int? PeekLastRemovedRecipe() =>
        throw new NotImplementedException("Part A: implement PeekLastRemovedRecipe.");

    public IReadOnlyList<int> GetCookingPlan() =>
        throw new NotImplementedException("Part A: implement GetCookingPlan.");

    public bool StartCooking(int recipeId) =>
        throw new NotImplementedException("Part A: implement StartCooking.");

    public string? PeekNextInstruction() =>
        throw new NotImplementedException("Part A: implement PeekNextInstruction.");

    public string? CompleteNextInstruction() =>
        throw new NotImplementedException("Part A: implement CompleteNextInstruction.");

    public IReadOnlyList<Recipe> SearchByTitle(string searchText) =>
        throw new NotImplementedException("Part B: implement SearchByTitle.");

    public IReadOnlyList<Recipe> SearchByIngredient(string searchText) =>
        throw new NotImplementedException("Part B: implement SearchByIngredient.");

    public IReadOnlyList<Recipe> GetHighestProteinRecipes(int count) =>
        throw new NotImplementedException("Part B: implement GetHighestProteinRecipes.");

    public bool AddSavedRecipe(int recipeId) =>
        throw new NotImplementedException("Part B: implement AddSavedRecipe.");

    public bool RemoveSavedRecipe(int recipeId) =>
        throw new NotImplementedException("Part B: implement RemoveSavedRecipe.");

    public bool IsRecipeSaved(int recipeId) =>
        throw new NotImplementedException("Part B: implement IsRecipeSaved.");

    public IReadOnlyList<int> GetSavedRecipes() =>
        throw new NotImplementedException("Part B: implement GetSavedRecipes.");
}
