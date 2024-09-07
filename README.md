## 各種スクリプトの説明

### ChatUIManager
実際にUnity内のオブジェクトに紐づけてあれこれする用のスクリプト。

### Models
ConnectionChatGPTで使う用のクラスをあれこれ定義している。

### ConnectionChatGPT
実際にChatGPTへAPIを投げているスクリプト。
```
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
```
ではそれぞれ
UniTaskとNewton.jsonのパッケージを利用しているので、PackageManagerからInstallすることを忘れないこと。