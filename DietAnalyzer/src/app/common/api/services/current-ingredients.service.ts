import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { CurrentIngredient } from "../../interfaces/current-ingredient.interface";

@Injectable()
export class CurrentIngredientsService
{
    url: string;

    constructor(private http: HttpClient){
        this.url = environment.API_URL + '/current-ingredient';
    }

    getCurrentIngredients() {
        return this.http.get<CurrentIngredient[]>(this.url, { withCredentials: true });
    }

    getCurrentIngredientById(id: number) {
        return this.http.get<CurrentIngredient>(this.url + '/' + id, { withCredentials: true });
    }

    getCurrentIngredientsByUserId(id: string) {
        return this.http.get<CurrentIngredient[]>(this.url + '/user/' + id, { withCredentials: true });
    }

    createCurrentIngredient(ingredient: CurrentIngredient) {
        var body = JSON.stringify(ingredient);
        var headerOptions = new HttpHeaders({'Content-Type':'application/json'});
        
        return this.http.post(this.url, body, {
            headers: headerOptions,
            withCredentials: true
        });
    }

    updateCurrentIngredient(ingredient: CurrentIngredient) {
        var body = JSON.stringify(ingredient);
        var headerOptions = new HttpHeaders({'Content-Type':'application/json'});

        return this.http.put(this.url, body, {
            headers: headerOptions,
            withCredentials: true
        });
    }

    deleteCurrentIngredient(id: number) {
        return this.http.delete(this.url + '/' + id, { withCredentials: true });
    }
}