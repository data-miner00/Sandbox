namespace Sandbox.Core.Models
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Customer
    {
        public double Earnings { get; set; }

        public string Name { get; set; }

        public double Scoring { get; set; }

        public string Email { get; set; }

        public bool IsSmoker { get; set; }

        public void UpdateScore()
        {
            this.Scoring = this.Earnings * (this.IsSmoker ? 0.8 : 1.0) * Random.Shared.NextDouble();
        }
    }
}
