import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MenuServiceService {
  private filteredMenuSource = new BehaviorSubject<any[]>([]);
  filteredMenu$ = this.filteredMenuSource.asObservable();

  setFilteredMenu(menu: any[]) {
    this.filteredMenuSource.next(menu);
  }
}
