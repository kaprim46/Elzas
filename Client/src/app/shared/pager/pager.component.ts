import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.scss']
})
export class PagerComponent {
  @Input() pageSize?: number;
  @Input() totalCount?: number;
  @Input() pageNumber?: number;
  @Output() pagedChanged = new EventEmitter<number>();

  onPagerCahnged(event: any){
    this.pagedChanged.emit(event.page);
  }
}
