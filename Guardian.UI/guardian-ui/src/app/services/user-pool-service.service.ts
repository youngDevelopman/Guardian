import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { UserPoolItem } from '../interfaces/user-pool-item.interface';
  
@Injectable()
export class UserPoolService{
    constructor(private http: HttpClient){}

    getUserPools(): Observable<UserPoolItem[]>{
        return this.http.get('https://localhost:5002/user-pool').pipe(
            map(response => response['userPools'] as UserPoolItem[])
        );
    }
}