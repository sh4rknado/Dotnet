   M S B A M L   `   `   `  ����G  CCisco_Script, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:Cisco_Script.ViewModelCisco_Script.ViewModel  5   	   )    #Cisco_Script.ViewModel.AjouterRoute   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:Cisco_Script.ViewModel     5   	     � Title$   Ajouter une nouvelle route��5      $	��228��6=   $	��422��6J   .��5      � .+�5	   
   �� $��Routing informations��6   $��
10,10,10,0q�61   $	��145��6E   $	��Top=�6R   .��5
      � .+�5      �� $��Addresse IP du réseaux :��6   $
��Left=�6;   $	��Top=�6V   $��	10,22,0,0q�6n   $	��151��6�   �� 5      $��Masque de sous réseaux :��6   $
��Left=�6;   $	��Top=�6V   $��	10,53,0,0q�6n   $	��151��6�   �� 5      $"��Interface sortie / next ip :��6   $
��Left=�6?   $	��Top=�6Z   $��	10,79,0,0q�6r   $	��151��6�   �� 5      -   5        $��	textboxip��5      $
��Left=�6+   $��23��6F   $��
166,25,0,0q�6R    �� TextWrapping$
 Wrap=�6f   $��192.168.1.0��6z   $	��Top=�6�   $	��181��6�    J� RenderTransformOrigin$ 0.5,0.5&�6�   �� 5      -   5        $��textboxcidr��5      $
��Left=�6-   $��23��6H   $��
166,53,0,0q�6T   $
 Wrap=�6h   $	��/24��6|   $	��Top=�6�   $	��181��6�   $ 0.5,0.5&�6�   �� 5      -   5        $��textboxinter��5      $
��Left=�6.   $��23��6I   $��
166,81,0,0q�6U   $
 Wrap=�6i   $��FastEthernet 0/1��6}   $	��Top=�6�   $	��181��6�   $ 0.5,0.5&�6�   5      5      �� 5   
   -   5        $��	Confirmer��5      $
��Left=�6%   $	��Top=�6@   $��75��6X   $��109,160,0,0q�6c   6x   �� 5   
   -   5        $��Annuler��5      $
��Left=�6#   $��75��6>   $��189,160,0,0q�6I   $��20��6^   $	��Top=�6j   6�   5      5      