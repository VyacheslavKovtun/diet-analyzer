import { HttpClient } from "@angular/common/http";
import { Injectable, OnInit } from "@angular/core";
import { environment } from "src/environments/environment";
import { FullRecipe } from "../../interfaces/full-recipe.interface";
import { RecipeInfo } from "../../interfaces/recipe-info.interface";
import { Recipe } from "../../interfaces/recipe.interface";

@Injectable()
export class RecipesService
{
    url: string;

    constructor(private http: HttpClient){
        this.url = environment.API_URL + '/recipes';
    }

    getRandomRecipes(amount: number) {
        return this.http.get<FullRecipe[]>(this.url + '/random/' + amount, { withCredentials: true });
    }

    getRecipesByTitle(title: string) {
        return this.http.get<Recipe[]>(this.url + '/search/title/' + title, {withCredentials: true});
    }

    getRecipeById(id: number) {
        return this.http.get<RecipeInfo>(this.url + '/search/id/' + id, {withCredentials: true});
    }
}