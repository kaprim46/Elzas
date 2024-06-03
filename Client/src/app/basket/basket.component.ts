import { Component } from '@angular/core';
import { BasketService } from './basket.service';
import { BasketItem } from '../shared/models/basket';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent {

  constructor(public basketServie: BasketService){}

  incrementQuantity(item: BasketItem){
    this.basketServie.addItemToBasket(item);
  }

  removeItem(event: {id: number, quantity: number}){
    this.basketServie.romveItemFromBasket(event.id, event.quantity);
  }

}
