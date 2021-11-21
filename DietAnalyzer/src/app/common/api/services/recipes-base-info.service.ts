import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { RecipeBaseInfo } from "../../interfaces/recipe-base-info.interface";

@Injectable()
export class RecipesBaseInfoService
{
    url: string;

    constructor(private http: HttpClient){
        this.url = environment.API_URL + '/recipe-base-info';
    }

    getRecipesBaseInfo() {
        return this.http.get<RecipeBaseInfo[]>(this.url, { withCredentials: true });
    }

    getRecipeBaseInfoById(id: number) {
        return this.http.get<RecipeBaseInfo>(this.url + '/' + id, { withCredentials: true });
    }

    createRecipeBaseInfo(info: RecipeBaseInfo) {
        var body = JSON.stringify(info);
        var headerOptions = new HttpHeaders({'Content-Type':'application/json'});
        
        return this.http.post(this.url, body, {
            headers: headerOptions,
            withCredentials: true
        });
    }

    updateRecipeBaseInfo(info: RecipeBaseInfo) {
        var body = JSON.stringify(info);
        var headerOptions = new HttpHeaders({'Content-Type':'application/json'});

        return this.http.put(this.url, body, {
            headers: headerOptions,
            withCredentials: true
        });
    }

    deleteRecipeBaseInfo(id: number) {
        return this.http.delete(this.url + '/' + id, { withCredentials: true });
    }
}