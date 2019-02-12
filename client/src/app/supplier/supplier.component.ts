import { Component, OnInit } from '@angular/core';

import { ApiService } from '../api.service';

@Component({
  selector: 'app-supplier',
  templateUrl: './supplier.component.html',
  styleUrls: ['./supplier.component.css']
})
export class SupplierComponent implements OnInit {

  constructor(private api: ApiService) { }

  ngOnInit() {
  }

}
