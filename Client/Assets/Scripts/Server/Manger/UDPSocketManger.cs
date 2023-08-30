using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;
using System.Net.Sockets;
using SocketProto;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using Google.Protobuf;
using Logging;
namespace Server
{
    class UDPSocketManger
    {
        private Socket client;
        //private Socket _socket => _client.Client;
        private static UDPSocketManger instance;
        public Action<MainPack> Handle;
        private IPEndPoint _localEnd;
        //private int localPort;
        private byte[] bytes;
        public static UDPSocketManger Instance
        {
            get
            {
                // ������ʵ���������򴴽�������ֱ�ӷ���
                if (instance == null)
                {
                    instance = new UDPSocketManger();
                }
                return instance;
            }
        }
        /// <summary>
        /// ��ʼ��Socket
        /// </summary>
        public string InitSocket()
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            try
            {
                client.Connect(NetConfigValue.ServiceIP, NetConfigValue.ServiceUDPPort);
                _localEnd = (IPEndPoint)client.LocalEndPoint;
              //  localPort = _localEnd.Port;
               // Debug.LogError(_localEnd.Address.ToString()+"  "+_localEnd.ToString());
                //_socket.Connect(new EndPoint(ip,6666))
               // Logging.HYLDDebug.Log(_localEnd+" ���ӳɹ� " + NetConfigValue.ServiceIP + " "+NetConfigValue.ServiceUDPPort);
                Logging.HYLDDebug.Trace($"UDP���ӳɹ�  ����:{client.LocalEndPoint}   ������:{client.RemoteEndPoint}");
                //HYLDManger.Instance.ShowMessage("���ӳɹ�");
                //HYLDStaticValue.�Ƿ�Ϊ����״̬ = true;

                Thread.Sleep(100);
                (new Thread(ReceiveMsg)).Start();
                //���ӳɹ�
            }
            catch (Exception e)
            {
                //����ʧ��
                //Logging.HYLDDebug.LogError(e);
                Logging.HYLDDebug.Log("����ʧ��" + e);
                HYLDManger.Instance.ShowMessage("����ʧ��");
                HYLDStaticValue.�Ƿ�Ϊ����״̬ = false;
            }
            return _localEnd.ToString();
        }
        private void ReceiveMsg()
        {
            Logging.HYLDDebug.LogError("��ʼ����");
           // IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(NetConfigValue.ServiceIP), localPort);

            //Socket client = o as Socket;
            while (true)
            {
                try
                {
                  //  Logging.HYLDDebug.LogError("udp���������� "+localPort);//
                    //bytes = _client.Receive(ref endpoint);
                  


                    ///�������淢�ͷ���ip�Ͷ˿ں�
                    EndPoint point = new IPEndPoint(IPAddress.Any, 0);
                    ///����ͻ��˽��յ�����Ϣ��С
                    byte[] bytes = new byte[1024];
                    ///���յ�����Ϣ��С(��ռ�ֽ���)
                    int length = client.Receive(bytes);
                    if (length == 0) return;
                    MainPack pack = (MainPack)MainPack.Descriptor.Parser.ParseFrom(bytes, 0, length);
                    //Logging.HYLDDebug.LogError("udp��������ing " + pack);//
                    Handle?.Invoke(pack);
                }
                catch(Exception ex)
                {

                    //_client = new UdpClient();
                    Logging.HYLDDebug.Trace($"udpClient  {_localEnd }���������쳣:"+ex.Message, true);
                    Debug.LogError("udpClient���������쳣:" + ex.Message+"   "+ _localEnd);
                   // Logging.HYLDDebug.Trace("udp����ʧ��:" + ex.Message);
                    // //_client.Connect(NetConfigValue.ServiceIP, NetConfigValue.ServiceUDPPort);
                    //IPEndPoint _localEnd = (IPEndPoint)_client.Client.LocalEndPoint;
                    //localPort = _localEnd.Port;
                    //_socket.Connect(new EndPoint(ip,6666))
                    //Logging.HYLDDebug.Log("���ӳɹ� " + localPort);
                    //HYLDManger.Instance.ShowMessage("���ӳɹ�");
                }
            }
        }
        /// <summary>
        /// ������Ҳ���
        /// </summary>
        public void SendOperation()
        {



            MainPack pack=new MainPack();
            pack.Requestcode = RequestCode.Battle;
            pack.Actioncode = ActionCode.BattlePushDowmPlayerOpeartions;
            pack.BattleInfo = new BattleInfo();
            pack.BattleInfo.SelfOperation = Manger.BattleData.Instance.selfOperation;
            pack.BattleInfo.OperationID = Manger.BattleData.Instance.sync_frameID+1;//����Ϊ������һ֡
            
            //if (pack.BattleInfo.SelfOperation.OpType ==  OperationType.Move)
            {
               // Logging.HYLDDebug.LogError("SendOperation:" + pack);
            }
            //Logging.HYLDDebug.LogError(" Send_operation()" +pack);
            Send(pack);
        }

        public void Send(MainPack pack)
        {
            byte[] sendbuff = pack.ToByteArray();
            try
            {
                ///��ȡIP��˿ں�
                EndPoint point = new IPEndPoint(IPAddress.Parse(NetConfigValue.ServiceIP), NetConfigValue.ServiceUDPPort);
                ///��������
               // string msg = textBox3.Text;
                ///�����ݷ��͵�ָ����ip�������Ķ˿�
                client.SendTo(sendbuff, point);


                //client.Send(sendbuff, sendbuff.Length);
                //				sendClient.Send (_mes,_mes.Length,sendEndPort);	
                //				Logging.HYLDDebug.Log("������:" + _mes.Length.ToString());
                //				Logging.HYLDDebug.Log("udp��������" + _mes.Length);
            }
            catch (Exception ex)
            {
                Logging.HYLDDebug.Log("udp����ʧ��:" + ex.Message);
                Logging.HYLDDebug.Trace("udp����ʧ��:" + ex.Message);
            }
        }
    }
}
