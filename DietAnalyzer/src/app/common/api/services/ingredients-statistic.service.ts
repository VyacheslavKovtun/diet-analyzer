import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { IngredientStatistic } from "../../interfaces/ingredient-statistic.interface";

@Injectable()
export class IngredientsStatisticService
{
    url: string;

    constructor(private http: HttpClient){
        this.url = environment.API_URL + '/ingredient-statistic';
    }

    getIngredientsStatistic() {
        return this.http.get<IngredientStatistic[]>(this.url, { withCredentials: true });
    }

    getIngredientsStatisticById(id: number) {
        return this.http.get<IngredientStatistic>(this.url + '/' + id, { withCredentials: true });
    }

    createIngredientsStatistic(statistic: IngredientStatistic) {
        var body = JSON.stringify(statistic);
        var headerOptions = new HttpHeaders({'Content-Type':'application/json'});
        
        return this.http.post(this.url, body, {
            headers: headerOptions,
            withCredentials: true
        });
    }

    updateIngredientsStatistic(statistic: IngredientStatistic) {
        var body = JSON.stringify(statistic);
        var headerOptions = new HttpHeaders({'Content-Type':'application/json'});

        return this.http.put(this.url, body, {
            headers: headerOptions,
            withCredentials: true
        });
    }

    deleteIngredientsStatistic(id: number) {
        return this.http.delete(this.url + '/' + id, { withCredentials: true });
    }
}