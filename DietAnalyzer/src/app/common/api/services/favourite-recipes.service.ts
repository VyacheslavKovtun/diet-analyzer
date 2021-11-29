import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { FavouriteRecipe } from "../../interfaces/favourite-recipe.interface";

@Injectable()
export class FavouriteRecipesService
{
    url: string;

    constructor(private http: HttpClient){
        this.url = environment.API_URL + '/favourite-recipe';
    }

    getFavouriteRecipes() {
        return this.http.get<FavouriteRecipe[]>(this.url, { withCredentials: true });
    }

    getFavouriteRecipeById(id: number) {
        return this.http.get<FavouriteRecipe>(this.url + '/' + id, { withCredentials: true });
    }

    getFavouriteRecipesByUserId(id: string) {
        return this.http.get<FavouriteRecipe[]>(this.url + '/user/' + id, { withCredentials: true });
    }

    getFavouriteRecipeByRecipeBaseInfoId(id: number) {
        return this.http.get<FavouriteRecipe>(this.url + '/recipe-base-info/' + id, { withCredentials: true });
    }

    createFavouriteRecipe(recipe: FavouriteRecipe) {
        var body = JSON.stringify(recipe);
        var headerOptions = new HttpHeaders({'Content-Type':'application/json'});
        
        return this.http.post(this.url, body, {
            headers: headerOptions,
            withCredentials: true
        });
    }

    updateFavouriteRecipe(recipe: FavouriteRecipe) {
        var body = JSON.stringify(recipe);
        var headerOptions = new HttpHeaders({'Content-Type':'application/json'});

        return this.http.put(this.url, body, {
            headers: headerOptions,
            withCredentials: true
        });
    }

    deleteFavouriteRecipe(id: number) {
        return this.http.delete(this.url + '/' + id, { withCredentials: true });
    }
}