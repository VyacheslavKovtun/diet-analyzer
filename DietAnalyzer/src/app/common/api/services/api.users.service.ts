import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { ApiUser } from "../../interfaces/api-user.interface";

@Injectable()
export class ApiUsersService
{
    url: string;

    constructor(private http: HttpClient){
        this.url = environment.API_URL + '/api-users';
    }

    getApiUsers() {
        return this.http.get<ApiUser[]>(this.url, { withCredentials: true });
    }

    getApiUserById(id: string) {
        return this.http.get<ApiUser>(this.url + '/' + id, { withCredentials: true });
    }

    createApiUser(user: ApiUser) {
        var body = JSON.stringify(user);
        var headerOptions = new HttpHeaders({'Content-Type':'application/json'});
        
        return this.http.post(this.url, body, {
            headers: headerOptions,
            withCredentials: true
        });
    }

    deleteApiUser(id: string) {
        return this.http.delete(this.url + '/' + id, { withCredentials: true });
    }
}