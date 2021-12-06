import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { ProductBaseInfo } from "../../interfaces/product-base-info.interface";

@Injectable()
export class ProductsBaseInfoService
{
    url: string;

    constructor(private http: HttpClient){
        this.url = environment.API_URL + '/product-base-info';
    }

    getProductsBaseInfo() {
        return this.http.get<ProductBaseInfo[]>(this.url, { withCredentials: true });
    }

    getProductBaseInfoById(id: number) {
        return this.http.get<ProductBaseInfo>(this.url + '/' + id, { withCredentials: true });
    }

    getProductBaseInfoByApiId(id: number) {
        return this.http.get<ProductBaseInfo>(this.url + '/api/' + id, { withCredentials: true });
    }

    createProductBaseInfo(info: ProductBaseInfo) {
        var body = JSON.stringify(info);
        var headerOptions = new HttpHeaders({'Content-Type':'application/json'});
        
        return this.http.post(this.url, body, {
            headers: headerOptions,
            withCredentials: true
        });
    }

    updateProductBaseInfo(info: ProductBaseInfo) {
        var body = JSON.stringify(info);
        var headerOptions = new HttpHeaders({'Content-Type':'application/json'});

        return this.http.put(this.url, body, {
            headers: headerOptions,
            withCredentials: true
        });
    }

    deleteProductBaseInfo(id: number) {
        return this.http.delete(this.url + '/' + id, { withCredentials: true });
    }
}