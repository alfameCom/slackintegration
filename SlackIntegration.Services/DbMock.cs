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
           label: 'Colleagues1',
           type: 'select',
           name: 'usernames1',
		   options: [
			   {
				label: 'Antti the great',
				value: 'antti'
			   },
			   {
				label: 'Matias the elder',
				value: 'matias'
			   },
			   {
			    label: 'David the pääarkkitehti',
				value: 'david'
			   },
			   {
			    label: 'Petteri the n00b',
				value: 'petteri'
			   }
		   ]
         },{
           label: 'Colleagues2',
           type: 'select',
           name: 'usernames2',
		   options: [
			   {
				label: 'Antti the great',
				value: 'antti'
			   },
			   {
				label: 'Matias the elder',
				value: 'matias'
			   },
			   {
			    label: 'David the pääarkkitehti',
				value: 'david'
			   },
			   {
			    label: 'Petteri the n00b',
				value: 'petteri'
			   }
		   ]
         },{
           label: 'Colleagues3',
           type: 'select',
           name: 'usernames3',
		   options: [
			   {
				label: 'Antti the great',
				value: 'antti'
			   },
			   {
				label: 'Matias the elder',
				value: 'matias'
			   },
			   {
			    label: 'David the pääarkkitehti',
				value: 'david'
			   },
			   {
			    label: 'Petteri the n00b',
				value: 'petteri'
			   }
		   ]
         },{
           label: 'Colleagues4',
           type: 'select',
           name: 'usernames4',
		   options: [
			   {
				label: 'Antti the great',
				value: 'antti'
			   },
			   {
				label: 'Matias the elder',
				value: 'matias'
			   },
			   {
			    label: 'David the pääarkkitehti',
				value: 'david'
			   },
			   {
			    label: 'Petteri the n00b',
				value: 'petteri'
			   }
		   ]
         },
         {
           label: 'Message',
           type: 'textarea',
           name: 'messsage'
         },
       ],
}
";
    }
}