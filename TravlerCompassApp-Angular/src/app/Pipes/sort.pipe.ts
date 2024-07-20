import { Pipe, PipeTransform } from '@angular/core'; //Imports the necessary Angular core modules to create and transform the pipe.

@Pipe({
  name: 'sort' // Define a new pipe with the name 'sort'
})
export class SortPipe implements PipeTransform {
 // Implement the transform method which is required by the PipeTransform interface
  transform(value: Array<string>, ...args: any[]): any { //Defines the transform method which takes two parameters
//value: The array to be sorted. //args: Additional arguments, which include the field to sort by and the sort direction.
    // Retrieve the sort field and direction from the arguments(user passes this agurments)
    const softField = args[0];
    const sortDirection = args[1];//Extracts the sort field and sort direction from the arguments.
  
    let multiplier = 1; // Initialize a multiplier for sorting order


     // If the sort direction is 'desc', set the multiplier to -1 for descending order
    if(sortDirection === 'desc'){
      multiplier = -1;
    }

    //Calls the sort method on the array, passing a comparison function.
    value.sort((a: any, b: any) => {  // Sort the array using the JavaScript Array sort method
     // Compare the two items based on the sortField and apply the multiplier
      if(a[softField] < b[softField]) {
        return -1 * multiplier;
      }else if(a[softField] > b[softField]){
        return 1 * multiplier;
      }else{
        return 0;
      }
      //Compares two items (a and b) based on the sortField.
      //Returns -1, 1, or 0, adjusted by the multiplier, to determine the sort order
      
      
    });
    // Return the sorted array
    return value;
  }

}
