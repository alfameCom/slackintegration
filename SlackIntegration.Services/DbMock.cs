namespace SlackIntegration.Services
{
    public static class DbMock
    {
        public static string GetValikko => @"
{
   'text': 'Welcome!',
   'attachments': [
       {
           'text': 'Please choose what to report',
           'fallback': 'Please contact the Pää-arkkitehti',
           'callback_id': 'wopr_command',
           'color': '#3AA3E3',
           'attachment_type': 'default',
           'actions': [
               {
                   'name': 'command',
                   'text': 'Success',
                   'type': 'button',
                   'value': 'success',
               },
               {
                   'name': 'command',
                   'text': 'Äänikirjabonus',
                   'type': 'button',
                   'value': 'aanikirjabonus'
               }
           ]
       }
   ]
}
";

        public static string GetSuccessDialog => @"
{
    title: 'Praise your colleagues!',
       callback_id: 'submit-success',
       submit_label: 'Submit',
       elements: [
         {
           label: 'Message',
           type: 'textarea',
           name: 'messsage'
         },
         {
           label: 'Colleague 1',
           type: 'select',
           name: 'usernames1',
           data_source: 'users'
         },{
           label: 'Colleague 2',
           type: 'select',
           name: 'usernames2',
           data_source: 'users',
           optional: true
         },{
           label: 'Colleague 3',
           type: 'select',
           name: 'usernames3',
           data_source: 'users',
           optional: true
         },{
           label: 'Colleague 4',
           type: 'select',
           name: 'usernames4',
           'data_source': 'users',
            optional: true
         },
		 {
           label: 'Colleague 5',
           type: 'select',
           name: 'usernames5',
           'data_source': 'users',
           optional: true
         }
       ],
}
";
    }
}