import { Component, OnInit } from '@angular/core';
import { UserPoolService } from '../services/user-pool-service.service';
import { UserPoolItem } from '../interfaces/user-pool-item.interface';

@Component({
  selector: 'app-user-pool-select',
  templateUrl: './user-pool-select.component.html',
  styleUrls: ['./user-pool-select.component.css'],
  providers:[
    UserPoolService
  ]
})
export class UserPoolSelectComponent implements OnInit {

  userPools: UserPoolItem[];
  constructor(private userPoolSerice: UserPoolService) { }

  ngOnInit(): void {
    this.userPoolSerice.getUserPools()
      .subscribe(x => this.userPools = x);
  }

}
