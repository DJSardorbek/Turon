   M S B A M L   `   `   `  ���� K  GSotuvPlatformasi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:SotuvPlatformasiSotuvPlatformasi       SotuvPlatformasi.EditPrice   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  *localclr-namespace:SotuvPlatformasi  -     � Title$  	EditPrice��$	��500��$	��600�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=�$��2q����#263859 � 	Resources  �� 
TargetType �� Property �� Value-  'System.Windows.Input.KeyboardNavigation �� 
TargetType	 �� CornerRadius
 �� Padding   ControlLightColor �� Color ��Name �� Triggers��%�  '��       '�� �     �� " ��.���  � ��$ Segoe UI)���  � ��$
 18px(���  � ��
 �������  � ��$ Center=���  � ��$ 30 0 0 0q��� " ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  �@ TabNavigation$
 None=���  �@�FocusVisualStyle Z� ��  � ��$	 120����  � ��$ 35����  �@J�	AllowDrop . ��  � ��
 �������  � ��$ Segoe UI)���  � ��$
 12px(���  �@��
CaretBrush
 �������  � �� �� " ��.R��� $��Border��$	 5��$
 30 10 10 10q�$��1q����� # �   ���� $ #263859��._��� $ PART_ContentHost��$��0q� .��� ���#17223B � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $��* �.+�� $��0��$��0��$��	0,50,0,10q�.+��� $��
Maxsulot :��� $��0��$��1��$��	0,50,0,10q�.+��� -    ��Name$ 
txtProduct�� �� 
IsReadOnly . $��50��$��	20 0 20 0q�� $��1��$��0��$��	0,20,0,10q�.+��� $��Avvalgi narx :��� $��1��$��1��$��	0,20,0,10q�.+��� -   $ txtOldPrice�� . $��50��$��	20 0 20 0q�� $��2��$��0��$��	0,20,0,10q�.+��� $��Val. o'lchov :��� $��2��$��1��$��	0,20,0,10q�.+��� -   $ 	txtVal_ul��$��50��$��	20 0 20 0q� . � $��3��$��0��$��	0,20,0,10q�.+��� $��Yangi narx :��� $��3��$��1��$��	0,20,0,10q�.+��� -   $ txtNewPrice��$��50��$��	20 0 20 0q�� $��4��$��2��$��0 40 0 0q� �� $��* ��� $��* �.+��� -    ��Name$ btnEdit��$��0��  SotuvHeader#��[ $��50��$	��150��.����  �� Orientation$ 
Horizontal=�.+�'  !MaterialDesignThemes.Wpf.PackIcon     Kind$ CircleArrows=�$��Center=�$��20��$��20��$��5 0 10 0q��� $��O'zgartirish��$��0 0 0 0q�$
��16px(��� -   $ 	btnCancel��$��1��#��[ $��50��$	��150��.���� $ 
Horizontal=�.+�  $ CancelCircleOutline=�$��Center=�$��20��$��20��$��5 0 10 0q��� $��Bekor qilish��$��0 0 0 0q�$
��16px(�