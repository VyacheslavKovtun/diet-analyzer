import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { IngredientBaseInfo } from "../../interfaces/ingredient-base-info.interface";

@Injectable()
export class IngredientsBaseInfoService
{
    url: string;

    constructor(private http: HttpClient){
        this.url = environment.API_URL + '/ingredient-base-info';
    }

    getIngredientsBaseInfo() {
        return this.http.get<IngredientBaseInfo[]>(this.url, { withCredentials: true });
    }

    getIngredientBaseInfoById(id: number) {
        return this.http.get<IngredientBaseInfo>(this.url + '/' + id, { withCredentials: true });
    }

    getIngredientBaseInfoByApiId(id: number) {
        return this.http.get<IngredientBaseInfo>(this.url + '/api/' + id, { withCredentials: true });
    }

    createIngredientBaseInfo(info: IngredientBaseInfo) {
        var body = JSON.stringify(info);
        var headerOptions = new HttpHeaders({'Content-Type':'application/json'});
        
        return this.http.post(this.url, body, {
            headers: headerOptions,
            withCredentials: true
        });
    }

    updateIngredientBaseInfo(info: IngredientBaseInfo) {
        var body = JSON.stringify(info);
        var headerOptions = new HttpHeaders({'Content-Type':'application/json'});

        return this.http.put(this.url, body, {
            headers: headerOptions,
            withCredentials: true
        });
    }

    deleteIngredientBaseInfo(id: number) {
        return this.http.delete(this.url + '/' + id, { withCredentials: true });
    }
}