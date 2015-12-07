using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace BasicTemplate.Commons
{
    public static class NotificationManager
    {
        //トースト通知を表示
        public static void SendBasicToast(string message)
        {
            //トースト通知のテンプレートXMLを取得
            var template = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);
            //XMLをデバッグ表示で確認する場合は以下のコード
            //Debug.WriteLine(template.GetXml());

            //テンプレートXMLを編集する
            template.GetElementsByTagName("text").First().InnerText = message;
            //トースト通知を表示
            var notification = new ToastNotification(template);
            ToastNotificationManager.CreateToastNotifier().Show(notification);
        }

        //ライブタイル通知を表示
        public static void SendBasicTile(string message)
        {
            var template = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Text01);
            Debug.WriteLine(template.GetXml());

            template.GetElementsByTagName("text").First().InnerText = message;
            var notification = new TileNotification(template);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(notification);

        }

        //バッジ通知を表示
        public static void SendBasicBadge(int number)
        {
            var template = BadgeUpdateManager.GetTemplateContent(BadgeTemplateType.BadgeNumber);
            Debug.WriteLine(template.GetXml());
            template.FirstChild.Attributes.First(q => q.NodeName == "value").InnerText = number.ToString();

            var notification = new BadgeNotification(template);
            BadgeUpdateManager.CreateBadgeUpdaterForApplication().Update(notification);
        }

        public static void ClearTileNotification()
        {
            TileUpdateManager.CreateTileUpdaterForApplication().Clear();
        }

        public static void ClearBadgeNotification()
        {
            BadgeUpdateManager.CreateBadgeUpdaterForApplication().Clear();
        }
    }
}
