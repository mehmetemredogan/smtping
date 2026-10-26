using System;
using System.IO;
using System.Text;
using MailKit;
using MailKit.Net.Smtp;

namespace SMTPing;

public class DelegateStream : Stream
{
    private readonly Action<string> _logger;

    public DelegateStream(Action<string> logger)
    {
        _logger = logger;
    }

    public override bool CanRead => false;
    public override bool CanSeek => false;
    public override bool CanWrite => true;
    public override long Length => 0;
    public override long Position { get => 0; set { } }

    public override void Flush() { }
    public override int Read(byte[] buffer, int offset, int count) => 0;
    public override long Seek(long offset, SeekOrigin origin) => 0;
    public override void SetLength(long value) { }

    public override void Write(byte[] buffer, int offset, int count)
    {
        var str = Encoding.UTF8.GetString(buffer, offset, count);
        _logger(str);
    }
}
