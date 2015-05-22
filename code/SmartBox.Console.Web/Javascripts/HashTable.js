var HashTable = function() {
    // HashTable存储对象
    var hashTableObj = new Object();
    
    // HashTable大小
    var _size = 0;
    
    // HashTable大小
         //调用方式：var size = hashTable.Size();
    this.Size = function () {
        return _size;
    }

    // 插入数据
         //调用方式：hashTable.Insert(1234, 'abcd');
    this.Insert = function (key , value) {
        if(!this.ContainsKey(key)) {
            _size++;
        }
        hashTableObj[key] = value;
    }
    
    // 读取指定键名的数据
         //调用方式：var value = hashTable.Get(1234);
    this.Get = function (key) {
        return this.ContainsKey(key) ? hashTableObj[key] : null;
    }
    
    // 删除指定键名的数据
         //调用方式：hashTable.Remove(1234);
    this.Remove = function ( key ) {
        if( this.ContainsKey(key) && ( delete hashTableObj[key] ) ) {
            _size--;
        }
    }
    
    // 清空HashTable
         //调用方式：hashTable.Clear();
    this.Clear = function () {
        _size = 0;
        hashTableObj = new Object();
    }
    
    // 是否包含指定键名的数据
         //调用方式：var hasKey = hashTable.ContainsKey(1234);
    this.ContainsKey = function ( key ) {
        return (key in hashTableObj);
    }
    
    // 是否包含指定值的数据
         //调用方式：var hasValue = hashTable.ContainsValue('abcd');
    this.ContainsValue = function ( value ) {
        for(var prop in hashTableObj) {
            if(hashTableObj[prop] == value) {
                return true;
            }
        }
        return false;
    }
    
    // 获取所有数据的值
         //调用方式：var values = hashTable.Values(); for(var i=values.length-1; i>=0; --i) { alert(values[i]); }
    this.Values = function () {
        var values = new Array();
        for(var prop in hashTableObj) {
            values.push(hashTableObj[prop]);
        }
        return values;
    }
    
  // 获取所有数据的键名
     //调用方式：var keys = hashTable.Keys(); for(var i=keys.length-1; i>=0; --i) { alert(keys[i]); }
    this.Keys = function () {
        var keys = new Array();
        for(var prop in hashTableObj) {
            keys.push(prop);
        }
        return keys;
    }
}

