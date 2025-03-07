namespace KRTsimbalov.Class
{
    static class MoveForm
    {
        static private Point mouseOffset;
        static private bool isMouseDown = false;

        static public void NewLocation(Form form, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                // Расчет нового положения формы
                Point newLocation = form.Location;
                newLocation.X += e.X - mouseOffset.X;
                newLocation.Y += e.Y - mouseOffset.Y;
                form.Location = newLocation;
            }
        }
        static public void MouseEnter(MouseEventArgs e)
        {
            // Запоминаем, что мышь зажата, и точку относительно левого верхнего угла формы
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = true;
                mouseOffset = new Point(e.X, e.Y);
            }
        }
        static public bool MouseDown()
        {
            return isMouseDown = false;
        }
    }
}
