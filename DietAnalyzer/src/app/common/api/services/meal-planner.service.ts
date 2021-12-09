import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable} from "@angular/core";
import { environment } from "src/environments/environment";
import { SavingRecipe } from "../../interfaces/saving-recipe.interface";
import { Day } from "../../interfaces/week-plan.interface";

@Injectable()
export class MealPlannerService
{
    url: string;

    constructor(private http: HttpClient){
        this.url = environment.API_URL + '/meal-planner';
    }

    getWeekPlan(startDate: string) {
        return this.http.get<Day[]>(this.url + '/week/' + startDate, { withCredentials: true });
    }

    getDayPlan(date: string) {
        return this.http.get<Day>(this.url + '/day/' + date, { withCredentials: true });
    }

    addRecipeToMealPlan(savingRecipe: SavingRecipe) {
        var body = JSON.stringify(savingRecipe);
        var headerOptions = new HttpHeaders({'Content-Type':'application/json'});
        
        return this.http.post(this.url, body, {
            headers: headerOptions,
            withCredentials: true
        });
    }

    deleteRecipeFromMealPlan(id: number) {
        return this.http.delete(this.url + '/delete/' + id, { withCredentials: true });
    }
}