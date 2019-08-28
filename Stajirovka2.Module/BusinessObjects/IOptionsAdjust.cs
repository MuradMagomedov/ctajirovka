using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stajirovka2.Module.BusinessObjects
{
	/// <summary>
	/// Интерфейс отвечающий за пересчет документа.
	/// Данный интерфейс позволяет запустить определнные ранние пересчеты в определнный момент времени, например при сохранении объекта (OnSaving)
	/// Потребность пересчета записывается ьитами в поле OptionsAdjust.
	/// Установка бита или его обнудение выполняется методами SetBit, SetZeroBit
	/// Запуск опреденного алгоритма пересчета выполняется методом Adjust_
	/// Запуск всех алгоритмов пересчета выполняется метором Adjust.
	/// Примером для использования данного интерфейса могут служить классы БазовыйРемонт, БазоваяРабота.
	/// !!!Важно. При измении каких-либо свойств связного объекта запускается пересчет у вышестоящего. Записывается бит пересчета (SetBit), 
	/// при этом нужно помнить что этот же пересчет может запускаться и при создании новой подчинненой записи для вышестоящего объекта (OnLoaded)
	/// или удалении (OnDeleting).
	/// !!! Реализация некоторых методов есть в классе OptionsAdjustHelper
	/// </summary>
	public interface IOptionsAdjust
	{
		/// <summary>
		/// Хранит биты пересчетов.
		/// </summary>
		long OptionsAdjust { get; set; }

		/// <summary>
		/// Установка бита пересчета
		/// </summary>
		/// <param name="bit">Номер бита.</param>
		void SetBit(byte bit);

		/// <summary>
		/// Снятие бита пересчета
		/// </summary>
		/// <param name="bit">Номер бита.</param>
		void SetBitZero(byte bit);

		/// <summary>
		/// Установливает все биты алгоритма пересчета по наименованию поля, которое было изменено.
		/// </summary>
		/// <param name="propertyName">Наименование поля (Шаблон: Наименование_класса.Наименование_поля)</param>
		void SetBit(string propertyName);

		/// <summary>
		/// Установка всех битов в 1
		/// </summary>
		void SetAllBit();

		/// <summary>
		/// Возвращает все биты для пересчета по переданному наименованию поля
		/// </summary>
		/// <param name="propertyName">Наименование поля (Шаблон: Наименование_класса.Наименование_поля)</param>
		long GetBit(string propertyName);

		/// <summary>
		/// Функция анализа установленных битов и запуска пересчетов
		/// </summary>
		void Adjust();

		/// <summary>
		/// Запуск алгоритма пересчета по биту
		/// </summary>
		/// <param name="bit">Номер бита</param>
		void Adjust(byte bit);

		/// <summary>
		/// Запуск алгоритма пересчета по наименованию
		/// </summary>
		/// <param name="propertyName">Наименование поля (Шаблон: Наименование_класса.Наименование_поля)</param>
		void Adjust(string propertyName);
	}

}
