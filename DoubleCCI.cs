// Copyright James Arlow / Jolly Wizard 2022

using System;
using System.Drawing;
using TradingPlatform.BusinessLayer;

namespace JollyWizard.Quantower.Indicators.DoubleCCI
{

    /// <summary>
    /// Double CCI Indicator
    /// </summary>
	public class DoubleCCI : Indicator
    {
        public static Color DefaultColor = Color.Transparent;

        public Indicator Indicator_CCI_Uno;
        public Indicator Indicator_CCI_Dos;

        public LineSeries LinePlot_CCI_Uno;
        public LineSeries LinePlot_CCI_Dos;

        const int DEFAULT_Transparency = 200;

        [InputParameter("CCI Period (Uno)")]
        public int CCI_Period_Uno = 34;

        [InputParameter("CCI Period (Dos)")]
        public int CCI_Period_Dos = 55;

        [InputParameter("CCI Price Type (Uno)"
        , 3
        , variants: new Object[] {
            "Open", PriceType.Open
        ,   "High", PriceType.High
        ,   "Close", PriceType.Close
        ,   "Low", PriceType.Low
        })]
        public PriceType CCI_PriceType_Uno;

        [InputParameter("CCI Price Type (Dos)"
        , 3
        , variants: new Object[] {
            "Open", PriceType.Open
        ,   "High", PriceType.High
        ,   "Close", PriceType.Close
        ,   "Low", PriceType.Low
        })]
        public PriceType CCI_PriceType_Dos;

        //[InputParameter("Default Width")]
        const int DefaultWidth = 1;
        

        //[InputParameter("Render Fill")]
        //public bool RenderFill = true;
        
        //[InputParameter("Bull Color")]
        //public Color Bull_Color = Color.FromArgb(DEFAULT_Transparency, Color.DarkGreen); // Very usage before implementing fill.

        //[InputParameter("Bear Color")]
        //public Color Bear_Color = Color.FromArgb(DEFAULT_Transparency, Color.DarkRed);


        /// <summary>
        /// Indicator's constructor.
        /// </summary>
        public DoubleCCI() : base()
        {
            // By default indicator will be applied to a separate window.
            this.SeparateWindow = true;

            LinePlot_CCI_Uno = AddLineSeries("CCI (Uno)", Color.Blue, DefaultWidth, LineStyle.Solid);
            LinePlot_CCI_Dos = AddLineSeries("CCI (Dos)", Color.Yellow, DefaultWidth + 1, LineStyle.Solid);

            // Defines indicator's name and description.
            Name = "Double CCI";
            Description = "Double CCI";
        }

        /// <summary>
        /// This function will be called after creating an indicator as well as after its input params reset or chart (symbol or timeframe) updates.
        /// </summary>
        protected override void OnInit()
        {
            Indicator_CCI_Uno = Core.Indicators.BuiltIn.CCI(CCI_Period_Uno, CCI_PriceType_Uno, MaMode.SMA);
            Indicator_CCI_Dos = Core.Indicators.BuiltIn.CCI(CCI_Period_Dos, CCI_PriceType_Dos, MaMode.SMA);

            AddIndicator(Indicator_CCI_Uno);
            AddIndicator(Indicator_CCI_Dos);
        }

        /// <summary>
        /// Calculations go here.
        /// </summary>
        protected override void OnUpdate(UpdateArgs args)
        {
            var CCI_Uno = Indicator_CCI_Uno.GetValue();
            var CCI_Dos = Indicator_CCI_Dos.GetValue();
            
            LinePlot_CCI_Uno.SetValue(CCI_Uno);
            LinePlot_CCI_Dos.SetValue(CCI_Dos);
        }
    }
}
