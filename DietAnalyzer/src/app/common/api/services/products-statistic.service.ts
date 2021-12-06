import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { ProductStatistic } from "../../interfaces/product-statistic.interface";

@Injectable()
export class ProductsStatisticService
{
    url: string;

    constructor(private http: HttpClient){
        this.url = environment.API_URL + '/product-statistic';
    }

    getProductsStatistic() {
        return this.http.get<ProductStatistic[]>(this.url, { withCredentials: true });
    }

    getProductStatisticById(id: number) {
        return this.http.get<ProductStatistic>(this.url + '/' + id, { withCredentials: true });
    }

    getProductStatisticByProductBaseInfoId(id: number) {
        return this.http.get<ProductStatistic>(this.url + '/prod-base-info/' + id, { withCredentials: true });
    }

    createProductStatistic(statistic: ProductStatistic) {
        var body = JSON.stringify(statistic);
        var headerOptions = new HttpHeaders({'Content-Type':'application/json'});
        
        return this.http.post(this.url, body, {
            headers: headerOptions,
            withCredentials: true
        });
    }

    updateProductStatistic(statistic: ProductStatistic) {
        var body = JSON.stringify(statistic);
        var headerOptions = new HttpHeaders({'Content-Type':'application/json'});

        return this.http.put(this.url, body, {
            headers: headerOptions,
            withCredentials: true
        });
    }

    deleteProductStatistic(id: number) {
        return this.http.delete(this.url + '/' + id, { withCredentials: true });
    }
}