using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartBox.Console.Common
{
    public class TreeRootDefine
    {
        /// <summary>
        /// 拼接字典树节点(初始化)
        /// </summary>
        /// <param name="item"></param>
        /// <param name="showbox"></param>
        /// <returns></returns>
        public static List<JsonTreeNode> CreateDictInfoTreeNode()
        {
            var nodes = new List<JsonTreeNode>();
            var node0 = new JsonTreeNode();
            node0.text = "1.选择插件类型";
            node0.id = "0";
            node0.hasChildren = false;
            node0.showcheck = false;
            node0.value = "0";
            node0.complete = true;
            nodes.Add(node0);
            var node = new JsonTreeNode();
            node.text = "2.上传插件";
            node.id = "1";
            node.hasChildren = false;
            node.showcheck = false;
            node.value = "1";
            node.complete = true;
            nodes.Add(node);
            var node1 = new JsonTreeNode();
            node1.text = "3.修改插件信息";
            node1.id = "2";
            node1.hasChildren = false;
            node1.showcheck = false;
            node1.value = "2";
            node1.complete = true;
            nodes.Add(node1);
            var node2 = new JsonTreeNode();
            node2.text = "4.保存/发布";
            node2.id = "3";
            node2.hasChildren = false;
            node2.showcheck = false;
            node2.value = "3";
            node2.complete = true;
            nodes.Add(node2);

            return nodes;
        }


        /// <summary>
        /// 拼接字典树节点(初始化)
        /// </summary>
        /// <param name="item"></param>
        /// <param name="showbox"></param>
        /// <returns></returns>
        public static List<JsonTreeNode> CreateMainDictInfoTreeNode()
        {
            var nodes = new List<JsonTreeNode>();
            var node0 = new JsonTreeNode();
            node0.text = "1.版本基本信息";
            node0.id = "0";
            node0.hasChildren = false;
            node0.showcheck = false;
            node0.value = "0";
            node0.complete = true;
            nodes.Add(node0);
            var node = new JsonTreeNode();
            node.text = "2.上传配置";
            node.id = "1";
            node.hasChildren = false;
            node.showcheck = false;
            node.value = "1";
            node.complete = true;
            nodes.Add(node);
            var node1 = new JsonTreeNode();
            node1.text = "3.修改配置信息";
            node1.id = "2";
            node1.hasChildren = false;
            node1.showcheck = false;
            node1.value = "2";
            node1.complete = true;
            nodes.Add(node1);
            var node2 = new JsonTreeNode();
            node2.text = "4.保存/发布";
            node2.id = "3";
            node2.hasChildren = false;
            node2.showcheck = false;
            node2.value = "3";
            node2.complete = true;
            nodes.Add(node2);

            return nodes;
        }
    }
}
