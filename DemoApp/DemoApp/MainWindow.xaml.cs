using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DemoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            SetSliderValue(0);
        }

        #region Slider

        private void SliderValueDidChange(Object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //First check if the sender is a slider
            if (sender is Slider)
            {
                //Call a function to set the new slider value to the label above it
                SetSliderValue((sender as Slider).Value); //The "as" keyword denotes a cast in C#
            }

        }

        /// <summary>
        /// Applies a new value to the slider label and sets that value to the slider
        /// </summary>
        /// <param name="newValue">Sets the new slider value and rotates the image</param>
        private void SetSliderValue(double newValue)
        {
            //Apply the new slider value
            DemoSlider.Value = newValue;

            //Apply the new slider value to the label above it
            //Fornat to 2 decimal places
            SliderValueLabel.Content = String.Format("{0:N2}", newValue);

            //Rotate image
            RotateImage(AlabamaImage, 360.0 * newValue);
        }

        private void DidClickApplySliderValue(object sender, RoutedEventArgs e)
        {
            //Try to parse the value from the text box
            try
            {
                var newSliderValue = Double.Parse(SliderValueTextBox.Text);

                //Check if value is within range and apply it to the slider
                ProcessSliderTextBoxValue(newSliderValue);
            }
            catch (Exception exception)
            {
                PrintSliderError(exception.Message);
            }


        }

        /// <summary>
        /// Performs validation on a potential new slider value before setting it
        /// </summary>
        /// <param name="value">New double value to be applied to the slider</param>
        private void ProcessSliderTextBoxValue(double value)
        {
            //If the new value is in the slider's range, apply it. Otherwise, show an error!
            if (value <= DemoSlider.Maximum && value >= DemoSlider.Minimum)
            {
                SetSliderValue(value);
            }
            else
            {
                PrintSliderError("Slider value out of range! Number must be between 0 and 1.");
            }
        }


        /// <summary>
        /// Prints an error regarding the slider
        /// </summary>
        /// <param name="error">String to be displayed as an error</param>
        private void PrintSliderError(string error)
        {
            SliderErrorLabel.Content = error;
        }

        #endregion

        #region CheckBox

        private void DidCheckSliderCheckBox(object sender, RoutedEventArgs e)
        {
            setUserInterfaceEnabled(false);
        }

        private void DidUncheckSliderCheckBox(object sender, RoutedEventArgs e)
        {
            setUserInterfaceEnabled(true);
        }

        /// <summary>
        /// Enables or disables the user interface elements
        /// </summary>
        /// <param name="enabled">The desired enabled state for the UI controls</param>
        private void setUserInterfaceEnabled(bool enabled)
        {
            DemoSlider.IsEnabled = enabled;
            SliderValueTextBox.IsEnabled = enabled;
            SliderApplyButton.IsEnabled = enabled;
        }

        #endregion

        #region Image

        /// <summary>
        /// Rotates a given image by a given angle
        /// </summary>
        /// <param name="imageToRotate">The windows image control to rotate</param>
        /// /// <param name="angleInDegrees">A double representing the angle to rotate a given image (in degrees)</param>
        private void RotateImage(System.Windows.Controls.Image imageToRotate, double angleInDegrees)
        {
            RotateTransform rotateTransform = new RotateTransform(angleInDegrees);
            imageToRotate.RenderTransform = rotateTransform;
        }

        #endregion

    }
}
