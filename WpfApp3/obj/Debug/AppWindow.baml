   M S B A M L   `   `   `  ����B  >WpfApp3, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp3WpfApp3  5   	       WpfApp3.AppWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp3     5   	     � Title$  	AppWindow��5   	   $	��450��6   $	��800��6(   .��5	      � ��5
   
   �� 5       �� ImageSource$ mainbackground.jpg��6    � ColumnDefinitions 5   
   �� 5      �� 5      �� 5       � RowDefinitions 5   
   �� 5      �� 5      �� 5      .+�5   
   �� $��2q�6   
������� �� CornerRadius$ 10��68   
�������$
��Left=�6d   $��Center=�6   ._�5      �� $
��Left=�6   $��Center=�64   $��8,5,8,5q�6O   .+�5      �� -   5        $��	TimeLabel��5      $
��Wrap=�6-   1236F   �� 5      -   5        $��IdLabel��5      $
��Wrap=�6+   1236D   �� 5      -   5        $��
LoginLabel��5      $
��Wrap=�6.   1236G   �� 5      -   5        $��	RoleLabel��5      $
��Wrap=�6-   1236F   �� 5      -   5        $��DateLastLoginLabel��5      $
��Wrap=�66   1236O   5      5      �� 5    
   $��50��6   $	��100��6    $��1��6/   $��3��6?   $��2q�6P   
�������$ 10��6w   
�������$��Center=�6�   $��Center=�6�   ._�5!      �� .+�5"      �� .��5#      �� .+�5$      �� -   5        $��MessagePanel��5$   %   $��50��69   5&      �� 5'       �� Orientation$ 
Horizontal=�6%   .+�5(      �� -   5        $��	MessageTB��5(   5   $	��100��6&   6H   �� 5)      -   5        $��SendBtn��5)   %   $��Bottom=�6H   $��Right=�6c   .��6   Отправить6�   5*      5+      5,      5-      5.      �� 50   
   $��50��6   $��50��6    $��2��6.   $��3��6>   $��2q�6O   
�������$ 10��6v   
�������$��Right=�6�   $��Center=�6�   ._�51      �� -	   5        $��UsersSP��51      .+�52      �� -
   5        $��usersCB��52      54      55      56      57      58      