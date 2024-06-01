import { Injectable, Inject, PLATFORM_ID } from '@angular/core'; //Injectable: Marks the service as injectable, allowing it to be provided and injected into other components or services.
import { isPlatformBrowser } from '@angular/common'; //A utility function that checks if the code is running in a browser environment.
import * as alertyfy from 'alertifyjs';//third party provider for alert notifications

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {
  private alertify: any; // A private property to hold the imported alertifyjs module.

constructor(@Inject(PLATFORM_ID) private platformId: Object) {
  if (isPlatformBrowser(this.platformId)) {
    import('alertifyjs').then((alertifyModule) => {
      this.alertify = alertifyModule.default;
    });
  }
}

 success(message:string){//success function that accepts a string
  if (this.alertify) {
    this.alertify.success(message);
  }
}

warning(message:string){//warning function that accepts a string
  if (this.alertify) {
    this.alertify.warninig(message);
  }
}
error(message:string){//error function that accepts a string
  if (this.alertify) {
    this.alertify.error(message);
  }
}
}
