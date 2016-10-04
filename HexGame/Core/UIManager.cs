using Microsoft.Xna.Framework;

using HexGame.UI;
using HexGame.Settings;
using HexGame.Core;


namespace HexGame.Core {
    class UIManager {
        public static BoxUIElement turnDisplay;
        public static BoxUIElement unitInfo;
        public static TextUIElement turnDisplayText;
        public static TextUIElement unitInfoText;
        public static TextUIElement unitInfoHeader;

        public static void DrawUI() {
            /* Turn display */
            turnDisplay = new BoxUIElement(GameManager.game);
            turnDisplay.Resize(300, 50);
            turnDisplay.SetPosition(Vector2.Zero);
            turnDisplay.BackgroundColor = Color.LightSlateGray;

            turnDisplayText = new TextUIElement(GameManager.game, FontManager.B15(GameManager.game), turnDisplay);
            turnDisplayText.AlignWithParent();

            /* Unit info */
            unitInfo = new BoxUIElement(GameManager.game);
            unitInfo.Resize(150, 200);
            unitInfo.SetPosition(new Vector2(
                0,
                VideoSettings.GetResolution().Y - unitInfo.height));
            unitInfo.BackgroundColor = Color.LightSlateGray;

            unitInfoHeader = new TextUIElement(GameManager.game, FontManager.B15(GameManager.game), unitInfo);
            unitInfoHeader.AlignWithParent();
            unitInfoHeader.Text = "Current unit";
            unitInfoText = new TextUIElement(GameManager.game, FontManager.B15(GameManager.game), unitInfo);
            unitInfoText.AlignWithParent();
            unitInfoText.ApplyOffset(new Vector2(0, 30));
        }
    }
}