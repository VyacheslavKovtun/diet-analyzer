import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { ForbiddenIngredient } from "../../interfaces/forbidden-ingredient.interface";

@Injectable()
export class ForbiddenIngredientsService
{
    url: string;

    constructor(private http: HttpClient){
        this.url = environment.API_URL + '/forbidden-ingredient';
    }

    getForbiddenIngredients() {
        return this.http.get<ForbiddenIngredient[]>(this.url, { withCredentials: true });
    }

    getForbiddenIngredientById(id: number) {
        return this.http.get<ForbiddenIngredient>(this.url + '/' + id, { withCredentials: true });
    }

    getForbiddenIngredientsByUserId(id: string) {
        return this.http.get<ForbiddenIngredient[]>(this.url + '/user/' + id, { withCredentials: true });
    }

    createForbiddenIngredient(ingredient: ForbiddenIngredient) {
        var body = JSON.stringify(ingredient);
        var headerOptions = new HttpHeaders({'Content-Type':'application/json'});
        
        return this.http.post(this.url, body, {
            headers: headerOptions,
            withCredentials: true
        });
    }

    updateForbiddenIngredient(ingredient: ForbiddenIngredient) {
        var body = JSON.stringify(ingredient);
        var headerOptions = new HttpHeaders({'Content-Type':'application/json'});

        return this.http.put(this.url, body, {
            headers: headerOptions,
            withCredentials: true
        });
    }

    deleteForbiddenIngredient(id: number) {
        return this.http.delete(this.url + '/' + id, { withCredentials: true });
    }
}