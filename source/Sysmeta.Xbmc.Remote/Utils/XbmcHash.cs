namespace Sysmeta.Xbmc.Remote.Utils
{
    using System;
    using System.Text;

    internal static class XbmcHash
    {
        public static string Hash(string input)
        {
            char[] chars = input.ToCharArray();
            for (int index = 0; index < chars.Length; index++)
            {
                if (chars[index] <= 127)
                {
                    chars[index] = Char.ToLowerInvariant(chars[index]);
                }
            }

            input = new string(chars);
            uint m_crc = 0xffffffff;
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            foreach (byte myByte in bytes)
            {
                m_crc ^= ((uint)(myByte) << 24);
                for (int i = 0; i < 8; i++)
                {
                    if ((Convert.ToUInt32(m_crc) & 0x80000000) == 0x80000000)
                    {
                        m_crc = (m_crc << 1) ^ 0x04C11DB7;
                    }
                    else
                    {
                        m_crc <<= 1;
                    }
                }
            }

            return String.Format("{0:x8}", m_crc);
        }
    }
}