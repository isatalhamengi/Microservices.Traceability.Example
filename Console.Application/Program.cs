using NLog;

Logger logger = LogManager.GetCurrentClassLogger();
System.Diagnostics.Trace.CorrelationManager.ActivityId = Guid.NewGuid();

Work1();
void Work1()
{
    Console.WriteLine("Work 1 Tetiklendi");
    logger.Debug("Work 1 Tetiklendi");
    Work2();
}

void Work2()
{
    Console.WriteLine("Work 2 Tetiklendi");
    logger.Debug("Work 2 Tetiklendi");
    Work3();
}

void Work3()
{
    Console.WriteLine("Work 3 Tetiklendi");
    logger.Debug("Work 3 Tetiklendi");
}