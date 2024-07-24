import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms'; // Imported FormBuilder and FormGroup from @angular/forms

import { Router } from '@angular/router';
import { TabsetComponent } from 'ngx-bootstrap/tabs'; // Imported TabsetComponent from ngx-bootstrap/tabs for tab functionality
import { IPackage } from '../../../../Interfaces/IPackage.interface'; // Imported IPackage interface from the specified path
import { Modelpackage } from '../../../../model/modelPackage';
import { Packages_dataService } from '../../../../services/packages_data.service';
import { AlertifyService } from '../../../../services/alertifyNotification/alertify.service';

@Component({
  selector: 'app-add-package', // Component selector
  templateUrl: './add-package.component.html', // Template URL for the component
  styleUrls: ['./add-package.component.css'] // Styles URL for the component
})
export class AddPackageComponent implements OnInit {
  //@ViewChild('Form') addPackageForm!: NgForm; // ViewChild for form element (commented out)
  addPackageForm!: FormGroup; // Declaration of addPackageForm as FormGroup
  @ViewChild('formTabs') formTabs!: TabsetComponent; // ViewChild for TabsetComponent
  @Input() property_pk!: IPackage; // Input property property_pk of type IPackage interface
  package = '1'; // Initial value for package (string)
  NextClicked!: boolean; // Declaration of NextClicked variable of type boolean (for tracking form navigation)
 
 // Initialize ModelPackage with default values
 packageView: Modelpackage = new Modelpackage();



  // Will come from Database later
  titleName: Array<string> = ['Thailand', 'Athens']; // Array of title names

  // Two-way binding setup for packageView object


  constructor(
    private router: Router, // Injected Router service
    private fb: FormBuilder, // Injected FormBuilder service
    private packageService: Packages_dataService,
    private alertif : AlertifyService
  ) {}

  ngOnInit() {
    this.CreateAddPackageForm(); // Initialization logic in ngOnInit to create form
    this.packageService.getAllPackagesAPI().subscribe(data => {
      console.log("Api Data:", data);
    })
  }

  // Method to create the addPackageForm FormGroup
  CreateAddPackageForm() {
    this.addPackageForm = this.fb.group({
      BasicInfo: this.fb.group({
        Tour: [null, Validators.required], // Form control for Tour with validation
        Description: [null, Validators.required] // Form control for Description with validation
      }),
      PriceInfo: this.fb.group({
        Price: [null, Validators.required] // Form control for Price with validation
      }),
      OtherInfo: this.fb.group({
        photo: [null, Validators.required]
        })
    });
  }

  // Getter method for accessing BasicInfo FormGroup within addPackageForm
  get BasicInfo() {
    return this.addPackageForm.controls['BasicInfo'] as FormGroup; // Using square bracket notation to access BasicInfo FormGroup
  }

  // Getter method for accessing PriceInfo FormGroup within addPackageForm
  get PriceInfo() {
    return this.addPackageForm.controls['PriceInfo'] as FormGroup; // Using square bracket notation to access PriceInfo FormGroup
  }

  get OtherInfo() {
    return this.addPackageForm.controls['OtherInfo'] as FormGroup;
  }

  // Method to navigate back to a previous route (example implementation)
  onBack() {
    this.router.navigate(['/']); // Navigate back to the root route ('/')
  }

  // Method called on form submission
  onSubmit() {
    this.NextClicked = true; // Set NextClicked to true if BasicInfo is invalid
  
    if (this.allTabsVaild()) {
      this.mapPackage();
      this.packageService.addPackage(this.packageView);
  
      console.log("Congrats, all forms are valid!");
      console.log(this.addPackageForm); // Log the entire addPackageForm FormGroup
      console.log(this.packageView); // Log the packageView object to verify values
      this.alertif.success("successfully Added your Package!")
       // If the package was successfully added, navigate to the home page
    const packageAddedSuccessfully = true; // Set this based on your logic or response from service
    if (packageAddedSuccessfully) {
      this.router.navigate(['home']);
    }
    } else {
      this.alertif.error("Please review the form and enter valid entries.");
    }
  
    console.log('SellRent=' + this.addPackageForm.value.BasicInfo.package); // Log form value for BasicInfo package
  }

  mapPackage():void {
      // Update packageView with values from the form
      this.packageView = new Modelpackage(
        1, // Assign a unique Id or fetch from the form if available
        this.BasicInfo.controls['Tour'].value,
        this.PriceInfo.controls['Price'].value,
        this.BasicInfo.controls['Description'].value,
        this.BasicInfo.controls['Tour'].value,
       
      );
  }
  allTabsVaild(): boolean{
       // Check if BasicInfo FormGroup is invalid
       if (this.BasicInfo.invalid) {
        this.formTabs.tabs[0].active = true; // Activate the first tab if BasicInfo is invalid
        return false; // Exit the function early if invalid
      }
  
      // Check if PriceInfo FormGroup is invalid
      if (this.PriceInfo.invalid) {
        this.formTabs.tabs[1].active = true; // Activate the second tab if PriceInfo is invalid
        return false; // Exit the function early if invalid
      } 

      if (this.OtherInfo.invalid) {
        this.formTabs.tabs[2].active = true;
        return false;
      }

      return true;
  }

  // Method to select a tab within the TabsetComponent
  selectTab(tabId: number, isCurrentTabValid: boolean) {
    this.NextClicked = true; // Set NextClicked to true when selecting a tab
    if (isCurrentTabValid) {
      this.formTabs.tabs[tabId].active = true; // Activate the specified tab if the current tab is valid
    }
  }
}