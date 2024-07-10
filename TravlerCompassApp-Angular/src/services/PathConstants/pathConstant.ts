

export const PathConstant = {
    ApiEndPoints: {},
    Patterns: {},
    menus: [
    {
        path:'agents',
        text: 'Agent',
        roles: ['Agent']
    },
     {
       path:'add-package',
      text: 'Add package',
      roles: ['Agent']
     },
    {
       path:'add-itinerary',
       text: 'Add itinerary',
        roles: ['Agent']
    },
    {
        path:'book-package',
        text: 'Book Package',
        roles: ['User']
    },
    {
        path:'book-itinerary',
        text: 'Book Itinerary',
        roles: ['User']
    },
   


     
]
}
/** 
 *  
 * {
        path:'agent-details/:id',
        text: 'agent-details/:id',
        roles: ['Agent' , 'User']
    },
    {
        path:'packageDetails/:id',
        text: 'packageDetails/:id',
        roles: ['Agent' , 'User']
    },
 * 
 * **/