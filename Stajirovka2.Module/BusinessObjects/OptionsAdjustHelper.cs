using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stajirovka2.Module.BusinessObjects
{
	/// <summary>
	/// Реализация методов интерфейса IOptionsAdjust
	/// </summary>
	public static class OptionsAdjustHelper
	{
		/// <summary>
		/// Установка бита пересчета
		/// </summary>
		/// <param name="bit">Номер бита.</param>
		public static long SetBit(long param, byte bit)
		{
			return param | (1L << bit);
		}

		/// <summary>
		/// Снятие бита пересчета
		/// </summary>
		/// <param name="bit">Номер бита.</param>        
		public static long SetBitZero(long param, byte bit)
		{
			return param & ~(1L << bit);
		}

		/// <summary>
		/// Установка битов пересчета
		/// </summary>
		/// <param name="instance">Ссылка на объект</param>
		/// <param name="propertyName">Наименование поля (Шаблон: Наименование_класса.Наименование_поля)</param>
		/// <returns>Объединенные биты пересчета</returns>
		public static void SetBit(IOptionsAdjust instance, string propertyName)
		{
			if (instance == null)
				return;

			long newBits = instance.GetBit(propertyName);
			instance.OptionsAdjust = instance.OptionsAdjust | newBits;
		}

		/// <summary>
		/// Установка всех битов в 1, кроме 0 (зарезервирован)
		/// </summary>
		public static long SetAllBit()
		{
			return (~0) << 1;
		}

		/// <summary>
		/// Запуск алгоритмов пересчета по установленным ранее битам (OptionsAdjust).
		/// </summary>
		/// <param name="instance"></param>
		public static void Adjust(IOptionsAdjust instance)
		{
			if (instance == null)
				return;

			if (instance.OptionsAdjust > 0)
			{
				for (byte i = 1; i <= 64; i++)
				{
					if ((instance.OptionsAdjust & (1 << i)) != 0)
					{
						instance.Adjust(i);
					}
				}
			}
		}

		/// <summary>
		/// Запуск алгоритмов пересчета по заданной маске битов.
		/// </summary>
		/// <param name="instance"></param>
		/// <param name="bits">Маска битов для пересчета</param>
		public static void AdjustFromBits(IOptionsAdjust instance, long bits)
		{
			if (instance == null)
				return;

			if (bits > 0)
			{
				for (byte i = 1; i <= 64; i++)
				{
					if ((bits & (1 << i)) != 0)
					{
						instance.Adjust(i);
					}
				}
			}
		}

		/// <summary>
		/// Запуск алгоритма пересчета по переданному полю.
		/// </summary>
		/// <param name="instance"></param>
		/// <param name="propertyName">Наименование поля (Шаблон: Наименование_класса.Наименование_поля)</param>
		public static void AdjustFromPropertyName(IOptionsAdjust instance, string propertyName)
		{
			if (instance == null)
				return;

			long bits = instance.GetBit(propertyName);
			OptionsAdjustHelper.AdjustFromBits(instance, bits);
		}
	}

}
