#region Using declarations
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Gui;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Gui.SuperDom;
using NinjaTrader.Gui.Tools;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using NinjaTrader.Core.FloatingPoint;

#endregion

//This namespace holds Optimization fitnesses in this folder and is required. Do not change it. 
namespace NinjaTrader.NinjaScript.OptimizationFitnesses
{
	public class SQN : OptimizationFitness
	{
		protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description									= @"System Quality Number (SQN)";
				Name										= "SQN";
			}
			else if (State == State.Configure)
			{
			}
		}

		protected override void OnCalculatePerformanceValue(StrategyBase strategy)
		{
			var averageProfitPerTrade = (strategy.SystemPerformance.AllTrades.TradesPerformance.GrossProfit+strategy.SystemPerformance.AllTrades.TradesPerformance.GrossLoss)/strategy.SystemPerformance.AllTrades.Count;
			double stddev = 0;
			
			foreach (Trade trade in strategy.SystemPerformance.AllTrades)
			{
				var tradeProfit = (trade.ProfitPoints*trade.Quantity*trade.Entry.Instrument.MasterInstrument.PointValue);
				stddev +=Math.Pow(tradeProfit-averageProfitPerTrade,2);
				
			}
			
			stddev /= strategy.SystemPerformance.AllTrades.Count;
			stddev = Math.Sqrt(stddev);
			
			Value = (Math.Sqrt(strategy.SystemPerformance.AllTrades.Count)*averageProfitPerTrade)/stddev;
			
			
			
		}

	}
}
