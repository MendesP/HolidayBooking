   M S B A M L   `   `   `  ����J  FHollydayBooking, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:HollydayBookingHollydayBooking  5   	       HollydayBooking.Invoice   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:HollydayBooking     5   	     � Title$  Invoice��5   	   $	��680��6   $	��486��6&   .��5	      � .+�5
   
   �� -   5         ��Name$ lbl_invoice��5
      $��INVOICE��6%   $
��Left=�67   $��
277,97,0,0q�6R   $	��Top=�6f   $	��186��6~   ���	#FFE4DDDD$��25��6�   $
��Bold"�6�   $��Raavi)�6�   �� 5   
   
���   �$��0q�6%   $
��Left=�69   $��63��6T   $��277,118,0,0q�6`   $	��Top=�6u   $	��186��6�   ���	#FFF5F5E66�   �� 5   
   -   5        $ lbl_invoiceDate��5      $��Date:��6)   $
��Left=�69   $��276,116,0,0q�6T   $	��Top=�6i   $
��Bold"�6�   �� 5   
   -   5        $ lbl_invoiceBookingID��5      $��Booking ID:��6.   $
��Left=�6D   $��276,135,0,0q�6_   $	��Top=�6t   $
��Bold"�6�   �� 5   
   -   5        $ lbl_customer��5      $��CUSTOMER��6&   $
��Left=�69   $��	11,97,0,0q�6T   $	��Top=�6g   $	��254��6   ���	#FFE4DDDD$��25��6�   $
��Bold"�6�   $��Raavi)�6�   �� 5   
   
���   �$��0q�6%   $
��Left=�69   $��63��6T   $��
11,118,0,0q�6`   $	��Top=�6t   $	��254��6�   ���	#FFF5F5E6._�5      �� -   5        $ lbl_costumerAddress��5      $��Address:��61   $
��Left=�6D   $	��Top=�6_   $
��Bold"�6w   $��	-1,36,0,0q�6�   5      �� 5   
   -   5        $ lbl_costumerID��5      $	��ID:��6(   $
��Left=�66   $��
11,116,0,0q�6Q   $	��Top=�6e   $
��Bold"�6}   �� 5   
   -   5        $ lbl_costumerName��5      $��Name:��6*   $
��Left=�6:   $��
11,135,0,0q�6U   $	��Top=�6i   $
��Bold"�6�   �� 5   
   -   5        $ lbl_napierHolydayBooking��5      $��Napier Holyday Booking��62   $
��Left=�6S   $��
110,29,0,0q�6n   $	��Top=�6�   $	��285��6�   $��20(�6�   $��Bookman Old Style)�6�   $
��Bold"�6�   �� 5   
   -	   5        $ lbl_invoiceDescription��5      $��Description��60   $
��Left=�6F   $��
10,222,0,0q�6a   $	��Top=�6u   $	��328��6�   ���	#FFE4DDDD$��32��6�   $
��Bold"�6�   $��Raavi)�6�   $��14(�6�   �� 5   
   -
   5        $ lbl_invoiceAmount��5      $��Amount��6+   $
��Left=�6<   $��343,222,0,0q�6W   $	��Top=�6l   $	��120��6�   ���	#FFE4DDDD$��25��6�   $
��Bold"�6�   $��Raavi)�6�   $��RightToLeft=�6�   $��14(�6�   �� 5   
   
���   �$��0q�6%   $
��Left=�69   $	��241��6T   $��
10,244,0,0q�6a   $	��Top=�6u   $	��328��6�   ���	#FFF5F5E66�   �� 5   
   
���   �$��0q�6%   $
��Left=�69   $	��241��6T   $��343,244,0,0q�6a   $	��Top=�6v   $	��120��6�   ���	#FFF5F5E66�   �� 5   
   -   5        $ lbl_bookingDescription��5      $�� ��60   $
��Left=�6;   $��
10,242,0,0q�6V   $	��Top=�6j   $	��327��6�   $	��243��6�   �� 5   
   -   5        $ lbl_bookingAmount��5      $�� ��6+   $
��Left=�66   $��343,244,0,0q�6Q   $	��Top=�6f   $	��120��6~   $	��241��6�   $��RightToLeft=�6�   �� 5   
   -   5        $ lbl_totalAmount��5      $��
Total Due:��6)   $
��Left=�6>   $��274,490,0,0q�6Y   $	��Top=�6n   $��77��6�   $
��Bold"�6�   �� 5   
   -   5        $ lbl_invoiceNumberOfGuests��5      $��#Guests:��63   $
��Left=�6F   $��276,154,0,0q�6a   $	��Top=�6v   $
��Bold"�6�   �� 5   
   -   5        $ lbl_customerIDOutput��5      $��cost ID��6.   $��Right=�6@   $��0,116,213,0q�6\   $	��Top=�6q   $	��196��6�   $��RightToLeft=�6�   �� 5   
   -   5        $ lbl_customerNameOutput��5      $��	cost Name��60   $��Right=�6D   $��0,135,213,0q�6`   $	��Top=�6u   $	��194��6�   $��RightToLeft=�6�   �� 5    
   -   5        $ lbl_customerAddressOutput��5       $��cost Address��63   $��Right=�6J   $��0,154,213,0q�6f   $	��Top=�6{   $	��190��6�   $��RightToLeft=�6�   �� 5!   
   -   5        $ lbl_invoiceDateOutput��5!      $��inv Date��6/   $��Right=�6B   $��
0,116,15,0q�6^   $	��Top=�6r   $	��106��6�   $��RightToLeft=�6�   �� 5"   
   -   5        $  lbl_invoiceBookingIDOutput��5"      $��inv Booking��64   $��Right=�6J   $��
0,135,15,0q�6f   $	��Top=�6z   $	��106��6�   $��RightToLeft=�6�   �� 5#   
   -   5        $% lbl_invoiceNumberOfGuestsOutput��5#      $��num of Guests��69   $��Right=�6Q   $��
0,154,15,0q�6m   $	��Top=�6�   $	��107��6�   $��RightToLeft=�6�   �� 5$   
   -   5        $" lbl_invoiceTotalAmountOutput��5$      $��total Amount��66   $��Right=�6M   $��
0,490,15,0q�6i   $	��Top=�6}   $��85��6�   $��RightToLeft=�6�   $
��Bold"�6�   �� 5%   
   -   5        $# lbl_napierHolydayBooking_Copy��5%      $!��Thank you for your purchase��67   $
��Left=�6]   $��119,572,0,0q�6x   $	��Top=�6�   $	��254��6�   $��16(�6�   $��Bookman Old Style)�6�   $
��Bold"�6�   5'      5(      