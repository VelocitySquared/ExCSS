﻿using System.IO;
using System.Linq;

namespace ExCSS
{
    public sealed class StyleRule : Rule, IStyleRule
    {
        public StyleRule(StylesheetParser parser) : base(RuleType.Style, parser)
        {
            AppendChild(SimpleSelector.All);
            AppendChild(new StyleDeclaration(this));
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            writer.Write(formatter.Style(SelectorText, Style));
        }

        public ISelector Selector
        {
            get => Children.OfType<ISelector>().FirstOrDefault();
            set => ReplaceSingle(Selector, value);
        }

        public string SelectorText
        {
            get => Selector.Text;
            set => Selector = Parser.ParseSelector(value);
        }

        public StyleDeclaration Style => Children.OfType<StyleDeclaration>().FirstOrDefault();
    }
}