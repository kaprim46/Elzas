import { Component, OnInit } from '@angular/core';
import { ShopService } from '../shop.service';
import { Product } from 'src/app/shared/models/product';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product-deatils',
  templateUrl: './product-deatils.component.html',
  styleUrls: ['./product-deatils.component.scss']
})
export class ProductDeatilsComponent implements OnInit{

  product?: Product;

  constructor(private shopServcie: ShopService, private activatedRoute: ActivatedRoute){}

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct(){
   const id = this.activatedRoute.snapshot.paramMap.get('id');

    if(id) this.shopServcie.getProduct(+id).subscribe({   //+ symbol to do cast coz id here is a string but we want a number
       next: product => this.product = product,
       error: error => console.log(error)
    });
  }

}
