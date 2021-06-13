import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-items',
  templateUrl: './items.component.html'
})
export class ItemsComponent {
  public items: IItemDTO[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<IItemDTO[]>(baseUrl + 'item/get').subscribe(result => {
      this.items = result;
    }, error => console.error(error));
  }
}

interface IItemDTO {
  id: number;
  name: string;
  description: string;
  initialPrice: number;
  finishAuctionTime: Date;
  requiredIncreaseAmount: number;
}

