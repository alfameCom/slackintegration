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
           'callback_id': 'success',
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
           label: 'Colleagues1',
           type: 'select',
           name: 'usernames1',
           data_source: 'users'
         },{
           label: 'Colleagues2',
           type: 'select',
           name: 'usernames2',
           data_source: 'users',
           optional: true
         },{
           label: 'Colleagues3',
           type: 'select',
           name: 'usernames3',
           data_source: 'users',
           optional: true
         },{
           label: 'Colleagues4',
           type: 'select',
           name: 'usernames4',
           'data_source': 'users',
            optional: true
         },
		 {
           label: 'Colleagues5',
           type: 'select',
           name: 'usernames5',
           'data_source': 'users',
           optional: true
         },
		 {
           label: 'Colleagues6',
           type: 'select',
           name: 'usernames6',
            'data_source': 'users',
            optional: true
         },
		 {
           label: 'Colleagues7',
           type: 'select',
           name: 'usernames7',
            'data_source': 'users',
            optional: true
         },
		 {
           label: 'Colleagues8',
           type: 'select',
           name: 'usernames8',
            'data_source': 'users',
            optional: true
         },
		 {
           label: 'Colleagues9',
           type: 'select',
           name: 'usernames9',
            'data_source': 'users',
            optional: true
         }
       ],
}
";
    }
}