import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { ProductInfo } from "../../interfaces/product-info.interface";
import { ShortProduct } from "../../interfaces/short-product.interface";

@Injectable()
export class ProductsService
{
    url: string;

    constructor(private http: HttpClient){
        this.url = environment.API_URL + '/products';
    }

    getProductsByTitle(title: string) {
        return this.http.get<ShortProduct[]>(this.url + '/search/title/' + title, {withCredentials: true});
    }

    getProductById(id: number) {
        return this.http.get<ProductInfo>(this.url + '/search/id/' + id, {withCredentials: true});
    }
}