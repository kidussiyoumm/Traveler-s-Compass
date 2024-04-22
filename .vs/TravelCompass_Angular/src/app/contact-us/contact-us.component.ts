import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-contact-us',
  templateUrl: './contact-us.component.html',
  styleUrls: ['./contact-us.component.css']
})
export class ContactUsComponent implements OnInit{
ngOnInit(): void {
  
}

onSubmit(Form : NgForm){
  console.log('works for now');
  console.log(Form);
}
}
