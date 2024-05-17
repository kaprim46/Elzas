import { NgModule } from '@angular/core';
import { ShopComponent } from './shop.component';
import { ProductDeatilsComponent } from './product-deatils/product-deatils.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {path: '', component: ShopComponent},
  {path: ':id', component: ProductDeatilsComponent}
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class ShopRoutingModule { }
