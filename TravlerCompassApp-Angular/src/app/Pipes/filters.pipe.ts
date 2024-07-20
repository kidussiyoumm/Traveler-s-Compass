import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filters'
})
export class FiltersPipe implements PipeTransform {
   // Implement the transform method which is required by the PipeTransform interface
   //we are passing from the templete to tell this pipe what we are going to filter 
  transform(value: any[], filterString: string, packageName: string): any[] {//this pipe can work with any type of array
    const resultArray = []; //the result, Initialize an empty array to store the filtered results
   
   
    // If the input array is empty, or the filter string or package name is empty, return the original array
    if(value.length === 0 || filterString === '' || packageName === ''){//we want to push all the filters to the result array 
       return value;
    }
    
    // Iterate over each item in the input array
    for(const item of value){

       // Check if the item's property (specified by packageName) matches the filterString
        if(item[packageName].toLowerCase().includes(filterString.toLowerCase())){
          resultArray. push(item);// If it matches, push the item to the result array
        }
    }

    return resultArray;  // Return the array containing the filtered results
   
  }

}
