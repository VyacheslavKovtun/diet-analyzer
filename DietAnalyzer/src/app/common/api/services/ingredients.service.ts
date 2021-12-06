import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { IngredientInfo } from "../../interfaces/ingredient-info.interface";
import { ShortIngredient } from "../../interfaces/short-ingredient.interface";

@Injectable()
export class IngredientsService
{
    url: string;

    constructor(private http: HttpClient){
        this.url = environment.API_URL + '/ingredients';
    }

    getIngredientsByName(name: string) {
        return this.http.get<ShortIngredient[]>(this.url + '/search/name/' + name, {withCredentials: true});
    }

    getIngredientById(id: number) {
        return this.http.get<IngredientInfo>(this.url + '/search/id/' + id, {withCredentials: true});
    }
}